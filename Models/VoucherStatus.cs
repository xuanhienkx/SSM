using System;

namespace SSM.Models
{
    public enum VoucherStatus:byte
    {
        [StringLabel("")]
        Pending = 0,
        [StringLabel("S")]
        Submited,
        [StringLabel("C")]
        Checked,
        [StringLabel("A")]
        Approved,
        [StringLabel("L")]
        Locked

    }

    public static class VoucherLable
    {
        public static String ViewStatus(VoucherStatus status)
        {
            switch (status)
            {
                case VoucherStatus.Pending: return " ";
                case VoucherStatus.Submited: return "S";
                case VoucherStatus.Checked: return "C";
                case VoucherStatus.Approved: return "A";
                case VoucherStatus.Locked: return "L";
                default: return " ";
            }
        }
    }
}