using ApplicationManagementSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ApplicationManagementSystem.BusinessLogic
{
    public class HoaDon_BUS
    {
        public DateTime NgayThanhToan { get; set; }
        public decimal SoTienThanhToan { get; set; }
        public string HinhThucThanhToan { get; set; }
        public Guid MaPhieuTTDT { get; set; }
        public string MaNhanVien { get; set; }

        public static void guiHoaDon (string TieuDe, string ThongTinHoaDon, string email)
        {
            try
            {
                // Địa chỉ email và mật khẩu của công ty
                string fromEmail = "congtyabcpttk@gmail.com";
                string fromPassword = "ctjz ctcl dtmj quwg"; 

                // Cấu hình SMTP client
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(fromEmail, fromPassword),
                    EnableSsl = true
                };

                // Tạo email message
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail),
                    Subject = TieuDe,
                    Body = ThongTinHoaDon,
                    IsBodyHtml = true // Nếu nội dung email là HTML
                };

                mailMessage.To.Add(email);

                // Gửi email
                client.Send(mailMessage);

                MessageBox.Show("Email đã được gửi thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi gửi email: " + ex.Message);
                // Xử lý ngoại lệ theo nhu cầu của bạn
            }
        }

        public static List<HoaDon_BUS> layDSHoaDon(Guid MaPhieuTTDT)
        {
            return HoaDon_DAO.layDSHoaDon(MaPhieuTTDT);
        }

        public static int themHoaDon(HoaDon_BUS hd)
        {
            HoaDon_DAO.themHoaDon(hd);
            return 0;
        }
    }
}
