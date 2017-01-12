using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MomPlusAdmin.Domain.Entity;
using MomPlusAdmin.Domain.Context;

namespace MomPlusAdmin.Processes
{
    public class BabyMethod
    {
        MomPlusDbContext context = new MomPlusDbContext();
        public BabyDev checkBMI(Guid id, Double weight, int height, DateTime bdate)
        {
          
            BabyDev bdev = context.BabyDev.Where(i => i.baby_id.Equals(id)).FirstOrDefault();
            var bmi = weight / height;
            var today = DateTime.Today;
            int babyMonth = 0;

            for (int no = 2; no <= 12; no++)
            {
                var TempMonth = bdate.AddMonths(no);
                var month = TempMonth.Month;
                    if(Convert.ToInt32(month) <= today.Month)
                    {
                        babyMonth = no;
                    }
            }
          
            switch(babyMonth)
            {
                case 2: bdev.second_month = Convert.ToDecimal(bmi); break;
                case 3: bdev.third_month = Convert.ToDecimal(bmi); break;
                case 4: bdev.fourth_month = Convert.ToDecimal(bmi); break;
                case 5: bdev.fift_month = Convert.ToDecimal(bmi); break;
                case 6: bdev.six_month = Convert.ToDecimal(bmi); break;
                case 7: bdev.seven_month = Convert.ToDecimal(bmi); break;
                case 8: bdev.eight_month = Convert.ToDecimal(bmi); break;
                case 9: bdev.nine_month = Convert.ToDecimal(bmi); break;
                case 10: bdev.ten_month = Convert.ToDecimal(bmi); break;
                case 11: bdev.eleven_month = Convert.ToDecimal(bmi); break;
                case 12: bdev.twelve_month = Convert.ToDecimal(bmi); break;
            }
           
            BabyDev dev = new BabyDev()
            {
                baby_id = id,
                id = bdev.id,
                first_month = bdev.first_month,
                second_month = bdev.second_month,
                third_month = bdev.third_month,
                fourth_month =bdev.fourth_month,
                fift_month =  bdev.fift_month,
                six_month = bdev.six_month,
                seven_month = bdev.seven_month,
                eight_month = bdev.eight_month,
                nine_month = bdev.nine_month,
                ten_month = bdev.ten_month,
                eleven_month = bdev.eleven_month,
                twelve_month = bdev.twelve_month,
            };
            return dev;
        }
    
    }
}