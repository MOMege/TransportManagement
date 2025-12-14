using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagement.Domain.Entites
{
    public class ApplicationUser : IdentityUser
    {
        // 🔹 الاسم بالكامل
        public string FullName { get; set; } = default!;

        // 🔹 نوع المستخدم (Admin - Driver - Accountant ...)
        public string RoleType { get; set; } = "User";

        // 🔹 حالة النشاط (موقّف أو فعال)
        public bool IsActive { get; set; } = true;

        // 🔹 تاريخ الإنشاء
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // 🔹 آخر تسجيل دخول
        public DateTime? LastLogin { get; set; }

        // 🔹 لعلاقة مع Driver Table (لو المستخدم هو السائق)
        public Guid? DriverId { get; set; }

        public void Deactivted()=> IsActive = false;
        public void Activated() => IsActive = true;
            

    }
}
