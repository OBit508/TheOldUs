using FungleAPI.Attributes;
using FungleAPI.Configuration.Attributes;
using FungleAPI.Translation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheOldUs.TOU
{
    public static class TouTranslation
    {
        public static StringNames HackerName;
        public static StringNames HackerBlur;
        public static StringNames HackerBlurMed;
        public static StringNames HitmanName;
        public static StringNames HitmanBlur;
        public static StringNames HitmanBlurMed;
        public static StringNames JailerName;
        public static StringNames JailerBlur;
        public static StringNames JailerBlurMed;
        public static StringNames MedicName;
        public static StringNames MedicBlur;
        public static StringNames MedicBlurMed;
        public static StringNames SheriffName;
        public static StringNames SheriffBlur;
        public static StringNames SheriffBlurMed;
        public static StringNames TimeMasterName;
        public static StringNames TimeMasterBlur;
        public static StringNames TimeMasterBlurMed;
        public static StringNames AcidMasterName;
        public static StringNames AcidMasterBlur;
        public static StringNames AcidMasterBlurMed;
        public static StringNames JanitorName;
        public static StringNames JanitorBlur;
        public static StringNames JanitorBlurMed;
        public static StringNames DiggerName;
        public static StringNames DiggerBlur;
        public static StringNames DiggerBlurMed;
        public static StringNames ArsonistName;
        public static StringNames ArsonistBlur;
        public static StringNames ArsonistBlurMed;
        public static StringNames JesterName;
        public static StringNames JesterBlur;
        public static StringNames JesterBlurMed;


        public static StringNames JesterWin;
        public static StringNames ArsonistWin;
        public static StringNames NotSkeld;


        public static StringNames Arrest;
        public static StringNames Clean;
        public static StringNames AcidTsunami;
        public static StringNames DigVent;
        public static StringNames EquipGun;
        public static StringNames Flame;
        public static StringNames Gasoline;
        public static StringNames Release;
        public static StringNames ReloadGun;
        public static StringNames Revive;
        public static StringNames Rewind;
        public static StringNames Shoot;
        public static StringNames Teleport;
        public static StringNames UnequipGun;
        public static StringNames UnlockVents;
        public static void SetTranslations()
        {
            SetRoles();
            SetWins();
            SetButtons();
            SetTranslationIDs();
        }
        public static void SetTranslationIDs()
        {
            TranslationManager.TranslationIDs.Add(, new Translator()
               .AddTranslation(SupportedLangs.Brazilian, ));


            TranslationManager.TranslationIDs.Add("hacker_teleportCooldown", new Translator("Teleport Cooldown")
                .AddTranslation(SupportedLangs.Brazilian, "Recarga do teleporte"));
            TranslationManager.TranslationIDs.Add("hacker_teleportDelay", new Translator("Teleport Delay")
                .AddTranslation(SupportedLangs.Brazilian, "Demora do teleporte"));
            TranslationManager.TranslationIDs.Add("hacker_teleportUses", new Translator("Teleporte Uses")
                .AddTranslation(SupportedLangs.Brazilian, "Usos do teleporte"));
            TranslationManager.TranslationIDs.Add("hacker_unlockVentsCooldown", new Translator("Unlock Vents Cooldown")
                .AddTranslation(SupportedLangs.Brazilian, "Recarga para desbloquear as ventilações"));
            TranslationManager.TranslationIDs.Add("hacker_unlockVentsDuration", new Translator("Unlock Vents Duration")
                .AddTranslation(SupportedLangs.Brazilian, "Duração do desbloqueio das ventilações"));
            TranslationManager.TranslationIDs.Add("hacker_unlockVentsUses", new Translator("Unlock Vents Uses")
                .AddTranslation(SupportedLangs.Brazilian, "Usos do desbloqueio das ventilações"));

            TranslationManager.TranslationIDs.Add("hitman_reloadCooldown", new Translator("Reload Cooldown")
               .AddTranslation(SupportedLangs.Brazilian, "Recarga do recarregamento"));
            TranslationManager.TranslationIDs.Add("hitman_reloadUses", new Translator("Reload Uses")
               .AddTranslation(SupportedLangs.Brazilian, "Máximo de recargas"));

            TranslationManager.TranslationIDs.Add("jailer_arrestCooldown", new Translator("Arrest Cooldown")
               .AddTranslation(SupportedLangs.Brazilian, "Recarga para prender"));
            TranslationManager.TranslationIDs.Add("jailer_arrestUses", new Translator("Arrest Uses")
               .AddTranslation(SupportedLangs.Brazilian, "Máximo de prisões"));
            TranslationManager.TranslationIDs.Add("jailer_releaseCooldown", new Translator("Release Cooldown")
               .AddTranslation(SupportedLangs.Brazilian, "Recarga para liberar"));


            TranslationManager.TranslationIDs.Add("tousettings_jail", new Translator("Jail")
                .AddTranslation(SupportedLangs.Brazilian, "Prisão"));
            TranslationManager.TranslationIDs.Add("tousettings_jail_arrestWhenEjected", new Translator("When ejected players go to jail")
                .AddTranslation(SupportedLangs.Brazilian, "Quando ejetados jogadores vão para a prisão"));
            TranslationManager.TranslationIDs.Add("tousettings_ship", new Translator("Ship")
                .AddTranslation(SupportedLangs.Brazilian, "Nave"));
            TranslationManager.TranslationIDs.Add("tousettings_ship_invertX", new Translator("Invert X")
                .AddTranslation(SupportedLangs.Brazilian, "Inverter X"));
            TranslationManager.TranslationIDs.Add("tousettings_ship_invertY", new Translator("Invert Y")
                .AddTranslation(SupportedLangs.Brazilian, "Inverter Y"));
            TranslationManager.TranslationIDs.Add("tousettings_ship_betterDoors", new Translator("Better Doors")
                .AddTranslation(SupportedLangs.Brazilian, "Portas melhoradas"));
        }
        public static void SetWins()
        {
            JesterWin = new Translator("Jester's victory")
                .AddTranslation(SupportedLangs.Brazilian, "Vitória do Palhaço").StringName;
            ArsonistWin = new Translator("Arsonis's victory")
                .AddTranslation(SupportedLangs.Brazilian, "Vitória do Incendiário").StringName;
            NotSkeld = new Translator("Only The Skeld map is supported")
                .AddTranslation(SupportedLangs.Brazilian, "Apenas o mapa The Skeld é suportado").StringName;
        }
        public static void SetButtons()
        {
            Arrest = new Translator("Arrest")
                .AddTranslation(SupportedLangs.Brazilian, "Prender").StringName;
            Clean = new Translator("Clean")
                .AddTranslation(SupportedLangs.Brazilian, "Limpar").StringName;
            AcidTsunami = new Translator("Acid")
                .AddTranslation(SupportedLangs.Brazilian, "Ácido").StringName;
            DigVent = new Translator("Dig Vent")
                .AddTranslation(SupportedLangs.Brazilian, "Cavar").StringName;
            EquipGun = new Translator("Equip Gun")
                .AddTranslation(SupportedLangs.Brazilian, "Equipar arma").StringName;
            Flame = new Translator("Ignition")
                .AddTranslation(SupportedLangs.Brazilian, "Ignição").StringName;
            Gasoline = new Translator("Gasoline")
                .AddTranslation(SupportedLangs.Brazilian, "Gasolina").StringName;
            Release = new Translator("Release")
                .AddTranslation(SupportedLangs.Brazilian, "Liberar").StringName;
            ReloadGun = new Translator("Reload")
                .AddTranslation(SupportedLangs.Brazilian, "Recarregar").StringName;
            Revive = new Translator("Revive")
                .AddTranslation(SupportedLangs.Brazilian, "Reviver").StringName;
            Rewind = new Translator("Rewind")
                .AddTranslation(SupportedLangs.Brazilian, "Rebobinar").StringName;
            Shoot = new Translator("Shoot")
                .AddTranslation(SupportedLangs.Brazilian, "Atirar").StringName;
            Teleport = new Translator("Teleport")
                .AddTranslation(SupportedLangs.Brazilian, "Teleportar").StringName;
            UnequipGun = new Translator("Unequip gun")
                .AddTranslation(SupportedLangs.Brazilian, "Desequipar arma").StringName;
            UnlockVents = new Translator("Unlock vents")
                .AddTranslation(SupportedLangs.Brazilian, "Desbloquear ventilações").StringName;
        }
        public static void SetRoles()
        {
            HackerName = new Translator("Hacker").StringName;
            HackerBlur = new Translator("Use your abilities to survive!")
                .AddTranslation(SupportedLangs.Brazilian, "Use suas abilidades para sobreviver!").StringName;
            HackerBlurMed = new Translator("You can teleport and unlock vents to survive.")
                .AddTranslation(SupportedLangs.Brazilian, "Você pode se teleportar e desbloquear as ventilações para sobreviver.").StringName;

            HitmanName = new Translator("Hitman").StringName;
            HitmanBlur = new Translator("Shoot the impostors.")
                .AddTranslation(SupportedLangs.Brazilian, "Atire nos impostores.").StringName;
            HitmanBlurMed = new Translator("Use your gun to shoot the impostors.")
                .AddTranslation(SupportedLangs.Brazilian, "Use a sua arma para atirar nos impostores.").StringName;

            JailerName = new Translator("Jailer")
                .AddTranslation(SupportedLangs.Brazilian, "Carcereiro").StringName;
            JailerBlur = new Translator("Arrest the impostors.")
                .AddTranslation(SupportedLangs.Brazilian, "Prenda os impostores.").StringName;
            JailerBlurMed = new Translator("Arrest the impostors to help the crew.")
                .AddTranslation(SupportedLangs.Brazilian, "Prenda os impostores para ajudar a tripulação.").StringName;

            MedicName = new Translator("Medic")
                .AddTranslation(SupportedLangs.Brazilian, "Médico").StringName;
            MedicBlur = new Translator("Revive dead bodies.")
                .AddTranslation(SupportedLangs.Brazilian, "Reviva corpos mortos.").StringName;
            MedicBlurMed = new Translator("Revive dead bodies and help them to tell who is the impostor.")
                .AddTranslation(SupportedLangs.Brazilian, "Reviva corpos mortos e os ajude a dizer quem são os impostores").StringName;

            SheriffName = new Translator("Sheriff")
                .AddTranslation(SupportedLangs.Brazilian, "Xerife").StringName;
            SheriffBlur = new Translator("Kill the impostors.")
                .AddTranslation(SupportedLangs.Brazilian, "Mate os impostores.").StringName;
            SheriffBlurMed = new Translator("Kill the impostors but if you kill a crewmate you die.")
                .AddTranslation(SupportedLangs.Brazilian, "Mate os impostores mas se você matar um tripulante você morre.").StringName;

            TimeMasterName = new Translator("Time Master")
                .AddTranslation(SupportedLangs.Brazilian, "Mestre do Tempo").StringName;
            TimeMasterBlur = new Translator("Rewind the time.")
                .AddTranslation(SupportedLangs.Brazilian, "Rebobine o tempo.").StringName;
            TimeMasterBlurMed = new Translator("Rewind the time and revert kills.")
                .AddTranslation(SupportedLangs.Brazilian, "Rebobine o tempo para reverter mortes.").StringName;

            AcidMasterName = new Translator("Acid Master")
                .AddTranslation(SupportedLangs.Brazilian, "Mestre do ácido").StringName;
            AcidMasterBlur = new Translator("Summon a acid tsunami.")
                .AddTranslation(SupportedLangs.Brazilian, "Sumone um tsunami ácido.").StringName;
            AcidMasterBlurMed = new Translator("You put acid on your victims and can summon a acid tsunami.")
                .AddTranslation(SupportedLangs.Brazilian, "Você coloca ácido nas suas vítimas e pode sumonar um tsunami ácido.").StringName;

            JanitorName = new Translator("Janitor")
                .AddTranslation(SupportedLangs.Brazilian, "Zelador").StringName;
            JanitorBlur = new Translator("Clean dead bodies.")
                .AddTranslation(SupportedLangs.Brazilian, "Limpe corpos mortos.").StringName;
            JanitorBlurMed = new Translator("Help the impostors cleaning dead bodies.")
                .AddTranslation(SupportedLangs.Brazilian, "Ajude os impostores limpando corpos mortos.").StringName;

            DiggerName = new Translator("Digger")
                .AddTranslation(SupportedLangs.Brazilian, "Escavador").StringName;
            DiggerBlur = new Translator("Dig vents.")
                .AddTranslation(SupportedLangs.Brazilian, "Cave ventilações").StringName;
            DiggerBlurMed = new Translator("Dig and connect vents.")
                .AddTranslation(SupportedLangs.Brazilian, "Cave e conecte ventilações.").StringName;

            ArsonistName = new Translator("Arsonist")
                .AddTranslation(SupportedLangs.Brazilian, "Incendiário").StringName;
            ArsonistBlur = new Translator("Put gasoline on others.")
                .AddTranslation(SupportedLangs.Brazilian, "Coloque gasolina nos outros.").StringName;
            ArsonistBlurMed = new Translator("Put gasoline on everyone and light it.")
                .AddTranslation(SupportedLangs.Brazilian, "Coloque gasolina em todos e acenda.").StringName;

            JesterName = new Translator("Jester")
                .AddTranslation(SupportedLangs.Brazilian, "Palhaço").StringName;
            JesterBlur = new Translator("Get ejected to win.")
                .AddTranslation(SupportedLangs.Brazilian, "Seja ejetado para ganahr.").StringName;
            JesterBlurMed = new Translator("Try to be suspicious to get ejected and win.").
                AddTranslation(SupportedLangs.Brazilian, "Tente ser suspeito para ser ejetado e ganhar.").StringName;
        }
    }
}
