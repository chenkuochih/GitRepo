using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi
{
    public interface IContactRep
    {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        IEnumerable<Contact> GetListAll();

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Contact GetByID(int id);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        Contact Add(Contact contact);

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="id"></param>
        void Remove(int id);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        bool Update(Contact contact);
    }
}
