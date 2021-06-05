using Family_Tree.Models;
using Family_Tree.ViewModels;
using FamilyTree_Web.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;

namespace FamilyTree_Web.Controllers
{
    public class HomeController : Controller
    {
        public static FamilyTree context;
        public static List<Family> Families;
        public static List<Models.Member> Siblings;
        public static List<int> DirectRelationsIds;
        public static int familyId = 0;
        public static int SelectedMemberId;
        public static int? childId;
        public HomeController()
        {
            context = new FamilyTree();
            Families = context.Families.ToList();
            ViewBag.Families = Families;
            familyId = context.Families.FirstOrDefault().Id;
            ViewBag.familyId = familyId;

            if (familyId != 0)
            {
                SelectedMemberId = context.Members.Where(m => m.Family.Id == familyId).FirstOrDefault().Id;
            }
            familyId = context.Members.Find(SelectedMemberId).Family.Id;
        }
        public ActionResult Index()
        {
            DirectRelationsIds = new List<int>();
            LoadFamilies();
            GetFamilyMembers();
            LoadPartner();
            LoadSelectMemberProfile();
            LoadSiblings(SelectedMemberId);
            LoadMother();
            LoadFather();
            LoadChilds();
            LoadIOtherRelations();
            return View();
        }

        private void LoadFamilies()
        {
            context = new FamilyTree();
           Families = context.Families.ToList();
            if (familyId == 0)
            {
                familyId = Families.FirstOrDefault().Id;
                ViewBag.Families = Families;
                ViewBag.familyId = familyId;
            }
        }

        private void GetFamilyMembers()
        {
            var members = context.Members.Where(i => i.FamilyId == familyId).ToList();
            ViewBag.FamilyMembers = members;
            var size = members.Count();         
        }

        //Save Siblings data to use it in another function
        private HashSet<Models.Member> GetSiblings(int? personId)
        {
            HashSet<Models.Member> result = new HashSet<Models.Member>();
            if (personId != null)
            {
                var SiblingRelatoins = context.RelationShips.Where(r => r.VirsualMember1.Id == personId || r.VirsualMember2.Id == personId).Where(r => r.Member1_RelationType == 4 || r.Member2_RelationType == 4).ToList();
                if (SiblingRelatoins != null)
                {
                    foreach (var item in SiblingRelatoins)
                    {
                        var Sibling = item.VirsualMember1.Id != personId ? item.VirsualMember1 : item.VirsualMember2;
                        result.Add(Sibling);
                        // get Siblings of sibling -- they are also siblings --
                        var OtherSiblingRelatoins = context.RelationShips.Where(r => (r.VirsualMember1.Id == Sibling.Id && r.VirsualMember1.Id != personId) || (r.VirsualMember2.Id == Sibling.Id && r.VirsualMember1.Id != personId)).Where(r => r.Member1_RelationType == 4 || r.Member2_RelationType == 4).ToList();
                        if (OtherSiblingRelatoins != null)
                        {
                            foreach (var item2 in OtherSiblingRelatoins)
                            {
                                var otherSibling = item2.VirsualMember1.Id != personId && item2.VirsualMember1.Id != Sibling.Id ? item2.VirsualMember1 : item2.VirsualMember2;
                                if (otherSibling.Id != personId) // to avoid insert selected member with siblings
                                {
                                    result.Add(otherSibling);
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        private void LoadPartner()
        {
            Models.Member Partner = new Models.Member();
            //direct Partner
            var PartnerRelatoin = context.RelationShips.Where(r => r.VirsualMember1.Id == SelectedMemberId || r.VirsualMember2.Id == SelectedMemberId).Where(r => r.Member1_RelationType == 1 || r.Member2_RelationType == 1).FirstOrDefault();
            if (PartnerRelatoin != null)
            {
                Partner = PartnerRelatoin.VirsualMember1.Id != SelectedMemberId ? PartnerRelatoin.VirsualMember1 : PartnerRelatoin.VirsualMember2;
                ViewBag.Partner = Partner;             
                DirectRelationsIds.Add(Partner.Id);
            }
            else
            {
                if (childId != null)
                {
                    //Get Mother of any child

                    if (!context.Members.Find(SelectedMemberId).Gender)
                    {
                        var motherofChild = context.RelationShips.Where(r => (r.VirsualMember1.Id == childId && r.Member1_RelationType == 3 && r.Member2_RelationType == 2) || (r.VirsualMember2.Id == childId && r.Member2_RelationType == 3 && r.Member1_RelationType == 2)).FirstOrDefault();
                        if (motherofChild == null)
                        {
                            var sibllingOfChild = GetSiblings(childId);
                            foreach (var item in sibllingOfChild)
                            {
                                motherofChild = context.RelationShips.Where(r => (r.VirsualMember1.Id == item.Id && r.Member1_RelationType == 3 && r.Member2_RelationType == 2) || (r.VirsualMember2.Id == item.Id && r.Member2_RelationType == 3 && r.Member1_RelationType == 2)).FirstOrDefault();
                                if (motherofChild != null)
                                {
                                    Partner = motherofChild.VirsualMember1.Id != item.Id ? motherofChild.VirsualMember1 : motherofChild.VirsualMember2;
                                    break;
                                }
                            }
                        }
                        if (motherofChild != null)
                        {
                            ViewBag.Partner = Partner;
                            DirectRelationsIds.Add(Partner.Id);
                        }
                    }
                    else
                    {
                        //Get Father of any child
                        var FatherofChild = context.RelationShips.Where(r => (r.VirsualMember1.Id == childId && r.Member1_RelationType == 3 && r.Member2_RelationType == 5) || (r.VirsualMember2.Id == childId && r.Member2_RelationType == 3 && r.Member1_RelationType == 5)).FirstOrDefault();
                        if (FatherofChild == null)
                        {
                            var sibllingOfChild = GetSiblings(childId);
                            foreach (var item in sibllingOfChild)
                            {
                                FatherofChild = context.RelationShips.Where(r => (r.VirsualMember1.Id == item.Id && r.Member1_RelationType == 3 && r.Member2_RelationType == 5) || (r.VirsualMember2.Id == item.Id && r.Member2_RelationType == 3 && r.Member1_RelationType == 5)).FirstOrDefault();
                                if (FatherofChild != null)
                                {
                                    Partner = FatherofChild.VirsualMember1.Id != item.Id ? FatherofChild.VirsualMember1 : FatherofChild.VirsualMember2;
                                    break;
                                }
                            }
                        }
                        if (FatherofChild != null)
                        {
                            ViewBag.Partner = Partner;
                            DirectRelationsIds.Add(Partner.Id);
                        }
                    }
                }
            }
        }

         //Selected member profile
        private void LoadSelectMemberProfile()
        {
            context = new FamilyTree();
            var selectedMember = context.Members.Find(SelectedMemberId);
            if (selectedMember != null) {
                ViewBag.selectedMember = selectedMember;            
            }
            LoadComPoMembers();
            LoadRelationTypes();
        }

        private void LoadSiblings(int? personId)
        {
            Siblings = new List<Models.Member>();
            HashSet<Models.Member> result = new HashSet<Models.Member>();
            if (personId != null)
            {
                var SiblingRelatoins = context.RelationShips.Where(r => r.VirsualMember1.Id == personId || r.VirsualMember2.Id == personId).Where(r => r.Member1_RelationType == 4 || r.Member2_RelationType == 4).ToList();
                if (SiblingRelatoins != null)
                {
                    foreach (var item in SiblingRelatoins)
                    {
                        var Sibling = item.VirsualMember1.Id != personId ? item.VirsualMember1 : item.VirsualMember2;
                        result.Add(Sibling);
                        // get Siblings of sibling -- they are also siblings --
                        var OtherSiblingRelatoins = context.RelationShips.Where(r => (r.VirsualMember1.Id == Sibling.Id && r.VirsualMember1.Id != personId) || (r.VirsualMember2.Id == Sibling.Id && r.VirsualMember1.Id != personId)).Where(r => r.Member1_RelationType == 4 || r.Member2_RelationType == 4).ToList();
                        if (OtherSiblingRelatoins != null)
                        {
                            foreach (var item2 in OtherSiblingRelatoins)
                            {
                                var otherSibling = item2.VirsualMember1.Id != personId && item2.VirsualMember1.Id != Sibling.Id ? item2.VirsualMember1 : item2.VirsualMember2;
                                if (otherSibling.Id != personId) // to avoid insert selected member with siblings
                                {
                                    result.Add(otherSibling);
                                }
                            }
                        }
                    }
                    ViewBag.Sibling = result;
                    foreach (var Sibling in result)
                    {                      
                        Siblings.Add(Sibling);
                        DirectRelationsIds.Add(Sibling.Id);
                    }
                }
            }
        }

        private void LoadMother()
        {
            //direct Mother Relation
            var MotherRelatoin = context.RelationShips.Where(r => (r.VirsualMember1.Id == SelectedMemberId && r.Member1_RelationType == 3 && r.Member2_RelationType == 2) || (r.VirsualMember2.Id == SelectedMemberId && r.Member2_RelationType == 3 && r.Member1_RelationType == 2)).FirstOrDefault();
            if (MotherRelatoin != null)
            {
                var mother = MotherRelatoin.VirsualMember1.Id != SelectedMemberId ? MotherRelatoin.VirsualMember1 : MotherRelatoin.VirsualMember2;
                ViewBag.Mother = mother;           
                DirectRelationsIds.Add(mother.Id);
            }
            else
            {
                //Get Mother of any sibling

                if (Siblings != null)
                {
                    foreach (var sibling in Siblings)
                    {
                        MotherRelatoin = context.RelationShips.Where(r => (r.VirsualMember1.Id == sibling.Id && r.Member1_RelationType == 3 && r.Member2_RelationType == 2) || (r.VirsualMember2.Id == sibling.Id && r.Member2_RelationType == 3 && r.Member1_RelationType == 2)).FirstOrDefault();
                        if (MotherRelatoin != null)
                        {
                            var mother = MotherRelatoin.VirsualMember1.Id != SelectedMemberId ? MotherRelatoin.VirsualMember1 : MotherRelatoin.VirsualMember2;
                            ViewBag.Mother = mother;
                            DirectRelationsIds.Add(mother.Id);
                            break;
                        }
                    }
                }
            }
        }

        private void LoadFather()
        {
            //direct Father Relation
            var FatherRelatoin = context.RelationShips.Where(r => (r.VirsualMember1.Id == SelectedMemberId && r.Member1_RelationType == 3 && r.Member2_RelationType == 5) || (r.VirsualMember2.Id == SelectedMemberId && r.Member2_RelationType == 3 && r.Member1_RelationType == 5)).FirstOrDefault();
            if (FatherRelatoin != null)
            {
                var Father = FatherRelatoin.VirsualMember1.Id != SelectedMemberId ? FatherRelatoin.VirsualMember1 : FatherRelatoin.VirsualMember2;
                ViewBag.Father = Father;
                DirectRelationsIds.Add(Father.Id);
            }
            else
            {
                //Get Father of any child
                if (Siblings != null)
                {
                    foreach (var sibling in Siblings)
                    {
                        FatherRelatoin = context.RelationShips.Where(r => (r.VirsualMember1.Id == sibling.Id && r.Member1_RelationType == 3 && r.Member2_RelationType == 5) || (r.VirsualMember2.Id == sibling.Id && r.Member2_RelationType == 3 && r.Member1_RelationType == 5)).FirstOrDefault();
                        if (FatherRelatoin != null)
                        {
                            var Father = FatherRelatoin.VirsualMember1.Id != sibling.Id ? FatherRelatoin.VirsualMember1 : FatherRelatoin.VirsualMember2;
                            ViewBag.Father = Father;
                            DirectRelationsIds.Add(Father.Id);
                            break;
                        }
                    }
                }
            }
        }


        private void LoadChilds()
        {
            var ChildRelatoin = context.RelationShips.Where(r => (r.VirsualMember1.Id == SelectedMemberId && r.Member2_RelationType == 3) || (r.VirsualMember2.Id == SelectedMemberId && r.Member1_RelationType == 3)).FirstOrDefault();
            if (ChildRelatoin != null)
            {
                var Child = ChildRelatoin.VirsualMember1.Id != SelectedMemberId ? ChildRelatoin.VirsualMember1 : ChildRelatoin.VirsualMember2;
                var childSiblings = GetSiblings(Child.Id);
                childSiblings.Add(Child);
                ViewBag.childs = childSiblings;
                foreach (var item in childSiblings)
                {
                    DirectRelationsIds.Add(item.Id);
                }
                childId = Child.Id;
            }
            else { childId = null; }
        }

        //Rest relations
        private void LoadIOtherRelations()
        {
            var otherRelatoins = context.Members.Where(m => m.Family.Id == familyId && !(DirectRelationsIds.Contains(m.Id)) && m.Id != SelectedMemberId).ToList();
            if (otherRelatoins != null)
            {
                ViewBag.otherRelatoins = otherRelatoins;             
            }
        }


        public ActionResult Click( int id )
        {
            SelectedMemberId = id;
            //SelectedMember = context.Members.Find(id);
                DirectRelationsIds = new List<int>();
                familyId = context.Members.Find(SelectedMemberId).Family.Id;
            //Refresh

            LoadFamilies();
            GetFamilyMembers();
            LoadSelectMemberProfile();
                LoadSiblings(SelectedMemberId);
                LoadChilds();
                LoadPartner();
                LoadMother();
                LoadFather();
                LoadIOtherRelations();
            

            return View("Index");
        }

        public ActionResult SelectFamily(int id)
        {
            
            familyId = id;
            if (context.Members.Where(m => m.FamilyId == id).Count() > 0)
                SelectedMemberId = context.Members.Where(m => m.Family.Id == id).FirstOrDefault().Id;
            else SelectedMemberId = 0;
            DirectRelationsIds = new List<int>();
            //Refresh

            LoadFamilies();
            GetFamilyMembers();
            LoadSelectMemberProfile();
            LoadSiblings(SelectedMemberId);
            LoadChilds();
            LoadPartner();
            LoadMother();
            LoadFather();
            LoadIOtherRelations();


            return View("Index");
        }


        [HttpPost]
        public  ActionResult AddMember( string FirstName , string LastName ,DateTime DOB,bool gender )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Models.Member member = new Models.Member();
                    member.DateOfBirth = DOB;
                    member.FamilyId = familyId;
                    member.FirstName = FirstName;
                    member.LastName = LastName;
                    member.Gender = gender;
                    context.Members.Add(member);
                    context.SaveChanges();

                }
                catch (Exception)
                {
                    TempData.Add("invalid", true);
                    ModelState.AddModelError(string.Empty, "Incorrect email/username or password");

                }
        

            }
            return RedirectToAction("Index");
        }



        [HttpPost]
        public ActionResult EditMember(string FirstName, string LastName, DateTime DOB, bool gender)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var member = context.Members.Find(SelectedMemberId);
                    member.DateOfBirth = DOB;
                    member.FirstName = FirstName;
                    member.LastName = LastName;
                    member.Gender = gender;
                    context.SaveChanges();

                }
                catch (Exception)
                {
                    TempData.Add("invalid", true);
                    ModelState.AddModelError(string.Empty, "Incorrect email/username or password");

                }


            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddFamily(string Name, string Comment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Models.Family family = new Models.Family();
                    family.Name = Name;
                    family.Comment = Comment;
                    context.Families.Add(family);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    TempData.Add("invalid", true);
                    ModelState.AddModelError(string.Empty, "Incorrect email/username or password");

                }


            }
            return RedirectToAction("Index");
        }
        private void LoadComPoMembers()
        {
            var members = context.Members.Where(m => m.Id != SelectedMemberId).ToList();
            List<ComboboxItem> data = new List<ComboboxItem>();

            foreach (var member in members)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = member.FirstName + " " + member.LastName;
                item.Value = member.Id;
                data.Add(item);
            }
            ViewBag.ComboMembers = data;
        }
        private void LoadRelationTypes()
        {
            var relationTypes = context.RelationTypes.ToList();
            List<ComboboxItem> data = new List<ComboboxItem>();

            foreach (var type in relationTypes)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = type.Name;
                item.Value = type.Id;
                data.Add(item);
            }
            ViewBag.ComboTypes = data;
        }


        [HttpPost]
        public ActionResult AddRelation(int MemberId, int TypeId)
        {
            if (ModelState.IsValid)
            {
                if (MemberId > 0 && TypeId > 0)
                {
                    RelationShip relation = new RelationShip();
                    relation.Member1 = SelectedMemberId;
                    relation.Member2 = MemberId;
                    relation.FamilyId = familyId;
                    relation.RelationStart = context.Members.Find(SelectedMemberId).DateOfBirth;
                    int type = TypeId;
                    switch (type)
                    {
                        case 1:
                            {
                                relation.Member1_RelationType = 1;
                                relation.Member2_RelationType = 1;
                                break;
                            }
                        case 2:
                            {
                                relation.Member1_RelationType = 3;
                                relation.Member2_RelationType = 2;
                                break;
                            }
                        case 3:
                            {
                                if (context.Members.Find(SelectedMemberId).Gender)
                                    relation.Member1_RelationType = 2;
                                else
                                    relation.Member1_RelationType = 5;
                                relation.Member2_RelationType = 3;
                                break;
                            }
                        case 4:
                            {
                                relation.Member1_RelationType = 4;
                                relation.Member2_RelationType = 4;
                                break;
                            }
                        case 5:
                            {
                                relation.Member1_RelationType = 3;
                                relation.Member2_RelationType = 5;
                                break;
                            }
                        case 6:
                            {
                                relation.Member1_RelationType = 9;
                                relation.Member2_RelationType = 6;
                                break;
                            }
                        case 7:
                            {
                                relation.Member1_RelationType = 7;
                                relation.Member2_RelationType = 7;
                                break;
                            }
                        case 8:
                            {
                                relation.Member1_RelationType = 9;
                                relation.Member2_RelationType = 8;
                                break;
                            }
                        case 9:
                            {
                                if (context.Members.Find(SelectedMemberId).Gender)
                                    relation.Member1_RelationType = 6;
                                else
                                    relation.Member1_RelationType = 8;
                                relation.Member2_RelationType = 9;
                                break;
                            }
                    }

                    context.RelationShips.Add(relation);
                    context.SaveChanges();
                    
                }
               
            }
            return RedirectToAction("Index");
        }

    }
}