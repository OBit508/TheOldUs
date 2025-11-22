using FungleAPI.Base.Roles;
using FungleAPI.Configuration.Attributes;
using FungleAPI.Role;
using FungleAPI.Role.Teams;
using FungleAPI.Translation;
using FungleAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TheOldUs.Roles.Neutrals
{
    internal class ArsonistRole : NeutralBase, ICustomRole
    {
        private static Dictionary<PlayerControl, List<PlayerControl>> soakedPlayers = new Dictionary<PlayerControl, List<PlayerControl>>();
        public static Dictionary<PlayerControl, List<PlayerControl>> SoakedPlayers
        {
            get
            {
                if (soakedPlayers == null)
                {
                    soakedPlayers = new Dictionary<PlayerControl, List<PlayerControl>>();
                }
                if (soakedPlayers.Count != PlayerControl.AllPlayerControls.Count)
                {
                    foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                    {
                        if (!soakedPlayers.ContainsKey(player))
                        {
                            soakedPlayers.Add(player, new List<PlayerControl>());
                        }
                    }
                }
                return soakedPlayers;
            }
        }
        [ModdedNumberOption("Gasoline Cooldown", 5, 50)]
        public static int GasolineCooldown => 20;
        public ModdedTeam Team { get; } = ModdedTeam.Neutrals;
        public StringNames RoleName { get; } = new Translator("Arsonist").StringName;
        public StringNames RoleBlur { get; } = new Translator("Put gas on others.").StringName;
        public StringNames RoleBlurMed { get; } = new Translator("Put gasoline on others and light it.").StringName;
        public StringNames RoleBlurLong { get; } = new Translator("The Arsonist needs to put gasoline on all the players in order to start a fire.").StringName;
        public Color RoleColor { get; } = new Color32(173, 95, 5, byte.MaxValue);
        public override Il2CppSystem.Collections.Generic.List<PlayerControl> GetValidTargets()
        {
            List<PlayerControl> list = GetTempPlayerList().ToSystemList();
            list.RemoveAll(t => t.Data.Role.GetTeam() == Team && !Team.FriendlyFire && SoakedPlayers[Player].Contains(t));
            return list.ToIl2CppList();
        }
    }
}
