using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;

namespace provider
{
    public class ContactRep : IContactRep
    {
        private List<Contact> list = new List<Contact>();
        public ContactRep()
        {
            list.Add(new Contact() { ID = 1, Age = 23, Birthday = Convert.ToDateTime("1977-05-30"), Name = "情缘", Sex = "男" });
            list.Add(new Contact() { ID = 2, Age = 55, Birthday = Convert.ToDateTime("1937-05-30"), Name = "令狐冲", Sex = "男" });
            list.Add(new Contact() { ID = 3, Age = 12, Birthday = Convert.ToDateTime("1987-05-30"), Name = "郭靖", Sex = "男" });
            list.Add(new Contact() { ID = 4, Age = 18, Birthday = Convert.ToDateTime("1997-05-30"), Name = "黄蓉", Sex = "女" });
        }

        public IEnumerable<Contact> GetListAll()
        {
            return list;
        }

        public Contact GetByID(int id)
        {
            return list.Find(item => item.ID == id);
        }

        public Contact Add(Contact contact)
        {
            if (contact == null)
            {
                throw new NullReferenceException("空引用异常");
            }
            int maxid = list.Max(item => item.ID);
            contact.ID = maxid + 1;
            list.Add(contact);
            return contact;
        }

        public void Remove(int id)
        {
            list.RemoveAll(item => item.ID == id);
        }

        public bool Update(Contact contact)
        {
            if (contact == null)
            {
                throw new NullReferenceException("空引用异常");
            }
            Remove(contact.ID);
            list.Add(contact);
            return true;
        }
    }
}
