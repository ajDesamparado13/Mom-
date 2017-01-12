using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MomPlusAdmin.Domain.Context;
using MomPlusAdmin.Domain.Entity;
using System.Data;
using System.Web.Mvc;
using MomPlusAdmin.Domain.Repository;

namespace MomPlusAdmin.Processes
{
    
    public class MedPersonnelMethod : Controller
    {
        MomPlusDbContext context = new MomPlusDbContext();
        MedPersonnelRepo repo = new MedPersonnelRepo();
        
        #region prenatal
        [HttpPost]
        public void CheckInputsPrenatal(PrenatalEntity prenatal, String admin_username)
        {

            MedPersonnelHistoryEntity toCreate = new MedPersonnelHistoryEntity();
            if (admin_username != null)
            {

                if (prenatal.tt1 != null)
                {
                    toCreate.p_gave_tt1 = admin_username.ToString();
                }
                if (prenatal.tt2 != null)
                {
                    toCreate.p_gave_tt2 = admin_username.ToString();
                }
                if (prenatal.tt3 != null)
                {
                    toCreate.p_gave_tt3 = admin_username.ToString();
                }
                if (prenatal.tt4 != null)
                {
                    toCreate.p_gave_tt4 = admin_username.ToString();
                }
                if (prenatal.tt5 != null)
                {
                    toCreate.p_gave_tt5 = admin_username.ToString();
                }
                if (prenatal.date_iron_was_given != null)
                {
                    toCreate.p_gave_iron = admin_username.ToString();
                }
                if (prenatal.date_given_vit_A != null)
                {
                    toCreate.p_gave_vit_a = admin_username.ToString();
                }
                 
                
                    MedPersonnelHistoryEntity toUpd = new MedPersonnelHistoryEntity()
                    {
                        user_id = prenatal.mother_id,
                        admin_who_registered = admin_username.ToString(),

                        p_gave_tt1 = toCreate.p_gave_tt1,
                        p_gave_tt2 = toCreate.p_gave_tt2,
                        p_gave_tt3 = toCreate.p_gave_tt3,
                        p_gave_tt4 = toCreate.p_gave_tt4,
                        p_gave_tt5 = toCreate.p_gave_tt5,
                        p_gave_vit_a = toCreate.p_gave_vit_a,
                        p_gave_iron = toCreate.p_gave_iron,

                    };
                    repo.AddPersonnel(toUpd);
                
            }
          }

        public void updatePrenatalMeds(PrenatalEntity prenatal, String admin_username)
        {
            MedPersonnelHistoryEntity upd = context.personnel.Where(
                          i => i.user_id.Equals(prenatal.mother_id)
                          ).OrderByDescending(i => i.id).FirstOrDefault();            
        
            
            if (admin_username != null)
            {
                    if (prenatal.tt1 != null)
                    {
                        upd.p_gave_tt1 = admin_username.ToString();
                    }
                    if (prenatal.tt2 !=     null)
                    {
                        upd.p_gave_tt2 = admin_username.ToString();
                    }
                    if (prenatal.tt3 != null)
                    {
                        upd.p_gave_tt3 = admin_username.ToString();
                    }
                    if (prenatal.tt4 != null)
                    {
                        upd.p_gave_tt4 = admin_username.ToString();
                    }
                    if (prenatal.tt5 != null)
                    {
                        upd.p_gave_tt5 = admin_username.ToString();
                    }
                    if (prenatal.date_iron_was_given != null)
                    {
                        upd.p_gave_iron = admin_username.ToString();
                    }
                    if (prenatal.date_given_vit_A != null)
                    {
                        upd.p_gave_vit_a = admin_username.ToString();
                    }

                    MedPersonnelHistoryEntity toUpd = new MedPersonnelHistoryEntity()
                    {
                        id = upd.id,
                        user_id = prenatal.mother_id,
                        admin_who_registered = upd.admin_who_registered,

                        p_gave_tt1 = upd.p_gave_tt1,
                        p_gave_tt2 = upd.p_gave_tt2,
                        p_gave_tt3 = upd.p_gave_tt3,
                        p_gave_tt4 = upd.p_gave_tt4,
                        p_gave_tt5 = upd.p_gave_tt5,
                        p_gave_vit_a = upd.p_gave_vit_a,
                        p_gave_iron = upd.pp_gave_iron,


                        pp_gave_iron = upd.pp_gave_iron,
                        pp_gave_vit_a = upd.pp_gave_vit_a,

                        i_6_to_12mos = upd.i_6_to_12mos,
                        i_12_to_59mos = upd.i_12_to_59mos,
                        i_12_to_59mos_dosage1 = upd.i_12_to_59mos_dosage1,
                        i_12_to_59mos_dosage2 = upd.i_12_to_59mos_dosage2,
                    };
                    repo.updatePersonnel(toUpd);
                }
        
        }
        #endregion

        #region postpartum

        [HttpPost]
        public void updatePostpartum(PostPartumEntity postpartum, String admin_username)
        {
            MedPersonnelHistoryEntity p_upd = context.personnel.Where(i => i.user_id.Equals(postpartum.mother_id)).OrderByDescending(i => i.id).FirstOrDefault();

            try
            {
                if (admin_username != null)
                {

                    if (postpartum.date_iron_was_given != null)
                    {
                        p_upd.pp_gave_iron = admin_username;
                    }
                    if (postpartum.date_vitA_was_given != null)
                    {
                        p_upd.pp_gave_vit_a = admin_username.ToString();
                    }
                    MedPersonnelHistoryEntity entity = new MedPersonnelHistoryEntity()
                    {
                        id = p_upd.id,
                        user_id = postpartum.mother_id,
                        admin_who_registered = p_upd.admin_who_registered,

                        p_gave_tt1 = p_upd.p_gave_tt1,
                        p_gave_tt2 = p_upd.p_gave_tt2,
                        p_gave_tt3 = p_upd.p_gave_tt3,
                        p_gave_tt4 = p_upd.p_gave_tt4,
                        p_gave_tt5 = p_upd.p_gave_tt5,
                        p_gave_vit_a = p_upd.p_gave_vit_a,
                        p_gave_iron = p_upd.pp_gave_iron,

                        pp_gave_iron = p_upd.pp_gave_iron,
                        pp_gave_vit_a = p_upd.pp_gave_vit_a,

                        i_6_to_12mos = p_upd.i_6_to_12mos,
                        i_12_to_59mos = p_upd.i_12_to_59mos,
                        i_12_to_59mos_dosage1 = p_upd.i_12_to_59mos_dosage1,
                        i_12_to_59mos_dosage2 = p_upd.i_12_to_59mos_dosage2,
                    };
                    repo.updatePersonnel(entity);
                }
            }
            catch (DataException /* dex */)
            {

                ModelState.AddModelError(string.Empty, "Opps, Something went wrong!");
            }
        }
        #endregion

        #region Immunization
        [HttpPost]
        public void updateImmunzation(ImmunizationEntity immunization, Guid id, String admin_username)
        {
            MedPersonnelHistoryEntity i_upd = context.personnel.Where(i => i.user_id.Equals(id)).OrderByDescending(i => i.id).FirstOrDefault(); ;

            if (admin_username != null)
            {

                if (immunization.six_to_twelvemos!= null)
                {
                    i_upd.i_6_to_12mos = admin_username.ToString();
                }
                if (immunization.twelve_to_59mos != null)
                {
                    i_upd.i_12_to_59mos = admin_username.ToString();
                }
                if (immunization.twelve59mos_dosage1 != null)
                {
                   i_upd.i_12_to_59mos_dosage1 = admin_username.ToString();
                }
                if (immunization.twelve59mos_dosage2 != null)
                {
                    i_upd.i_12_to_59mos_dosage2 = admin_username.ToString();
                }
                MedPersonnelHistoryEntity entity = new MedPersonnelHistoryEntity()
                {
                    id = i_upd.id,
                    user_id = id,

                    p_gave_tt1 = i_upd.p_gave_tt1,
                    p_gave_tt2 = i_upd.p_gave_tt2,
                    p_gave_tt3 = i_upd.p_gave_tt3,
                    p_gave_tt4 = i_upd.p_gave_tt4,
                    p_gave_tt5 = i_upd.p_gave_tt5,
                    p_gave_vit_a = i_upd.p_gave_vit_a,
                    p_gave_iron = i_upd.pp_gave_iron,

                    pp_gave_iron = i_upd.pp_gave_iron,
                    pp_gave_vit_a = i_upd.pp_gave_vit_a,

                    admin_who_registered = i_upd.admin_who_registered,
                    i_6_to_12mos = i_upd.i_6_to_12mos,
                    i_12_to_59mos = i_upd.i_12_to_59mos,
                    i_12_to_59mos_dosage1 = i_upd.i_12_to_59mos_dosage1,
                    i_12_to_59mos_dosage2 = i_upd.i_12_to_59mos_dosage2,
                };
                repo.updatePersonnel(entity);
            }
        }

        #endregion

    }
}