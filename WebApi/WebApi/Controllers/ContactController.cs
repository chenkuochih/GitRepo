using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ContactController : ApiController
    {
        Contact[] contacts = new Contact[]{
            new Contact(){ ID=1, Age=29, Birthday=Convert.ToDateTime("1990-11-06"), Name="吴亦凡", Sex="男"},
            new Contact(){ ID=2, Age=32, Birthday=Convert.ToDateTime("1987-05-04"), Name="李易峰", Sex="男"},
            new Contact(){ ID=3, Age=21, Birthday=Convert.ToDateTime("1998-02-14"), Name="王春力", Sex="男"},
            new Contact(){ ID=4, Age=19, Birthday=Convert.ToDateTime("2000-05-03"), Name="陈仟雅", Sex="女"},
        };
        /// <summary>
        /// /api/Contact
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Contact> GetListAll()
        {
            return contacts;
        }

        /// <summary>
        /// /api/Contact/id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Contact GetContactByID(int id)
        {
            Contact contact = contacts.FirstOrDefault<Contact>(item => item.ID == id);
            if (contact == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return contact;
        }

        /// <summary>
        /// 根据性别查询
        /// /api/Contact?sex=女
        /// </summary>
        /// <param name="sex"></param>
        /// <returns></returns>
        public IEnumerable<Contact> GetListBySex(string sex)
        {
            return contacts.Where(item => item.Sex == sex);
        }
    }
}
