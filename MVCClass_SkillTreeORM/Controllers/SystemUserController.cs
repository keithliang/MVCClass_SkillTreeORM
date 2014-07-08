using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCClass_SkillTreeORM.Models;
using System.ComponentModel;                  //MetadataType使用
using System.ComponentModel.DataAnnotations; //MetadataType使用

namespace MVCClass_SkillTreeORM.Controllers
{
    public class SystemUserController : Controller
    {      
        private MVCClassORMDatabaseEntities db = new MVCClassORMDatabaseEntities();

        [MetadataType(typeof(DataValidationDefinition))]
        public  class DataValidationDefinition
        //未於EF, Entity Framework設定比對條件
        {

        }

        //
        // GET: /SystemUser/

        public ActionResult Index()
        {
            return View(db.SystemUsers.Take(10));
        }

        //
        // GET: /SystemUser/Details/5

        public ActionResult Details(Guid? id)
            
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                //如果ID不存在則回傳一個HttpStatusCode
            }

            SystemUser systemuser = db.SystemUsers.Find(id);
            if (systemuser == null)
            {
                return HttpNotFound();
            }
            return View(systemuser);
        }

        //
        // GET: /SystemUser/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SystemUser/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "UpdateUser")]SystemUser systemuser)
        {
        //明確指明排除對象

            if (ModelState.IsValid)
            {
                systemuser.ID = Guid.NewGuid();
                systemuser.CreateDate = DateTime.Now;
                systemuser.UpdateDate = DateTime.Now;
                //得知使用者輸入的值是否符合規定
                //假若是的話
                //ID指名要唯一序號
                //自動填入時間

                try
                {
                    db.SystemUsers.Add(systemuser);
                    db.SaveChanges();
                    //例外處理
                    //狀況變更時
                    //資料儲存
                }
                catch (Exception)
                {
                    return View();
                    //不處理例外，僅顯示當下頁面
                }

                return RedirectToAction("Index");
                //導回Index
            }
            return View(systemuser);
        }

        //
        // GET: /SystemUser/Edit/5

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                //如果ID不存在則回傳一個System.Net.HttpStatusCode
            }

            SystemUser systemuser = db.SystemUsers.Find(id);
            if (systemuser == null)
            {
                return HttpNotFound();
            }
            return View(systemuser);
        }

        //
        // POST: /SystemUser/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SystemUser systemuser)
        {
            if (ModelState.IsValid)
            {
                var user = db.SystemUsers.FirstOrDefault(x => x.ID == systemuser.ID);
                //建立一個user物件
                //取得資料庫中符合條件的第一筆資料
                //條件為systemuser資料表中的ID

                if (user == null)
                {
                    return View(systemuser);
                }
                //假若user物件等於null
                //回傳資料

                user.Name = systemuser.Name;
                user.Account = systemuser.Account;
                user.Password = systemuser.Password;
                user.Email = systemuser.Email;
                user.UpdateDate = DateTime.Now;
                //User物件對應到的資料表資料
                //其中user.UpdateDate對應到函數DateTime.Now
                try
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    //例外處理
                    //User物件狀態已經修改
                    //回存資料
                }
                catch (Exception)
                {
                    return View();
                    //不處理例外，僅顯示當下頁面
                }
                return RedirectToAction("Index");
                //導回Index頁面
            }
            return View(systemuser);
            //導回頁面同時讀取systemuser
        }

        //
        // GET: /SystemUser/Delete/5

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                //如果ID不存在則回傳一個System.Net.HttpStatusCode
            }

            SystemUser systemuser = db.SystemUsers.Find(id);
            if (systemuser == null)
            {
                return HttpNotFound();
            }
            return View(systemuser);
        }

        //
        // POST: /SystemUser/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SystemUser systemuser = db.SystemUsers.Find(id);
            db.SystemUsers.Remove(systemuser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            //若資料庫連線需要要關閉，則允許關閉
            base.Dispose(disposing);
        }
    }
}