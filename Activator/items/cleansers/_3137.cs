﻿using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace Activator.Items.Cleansers
{
    class _3137 : item
    {
        internal override int Id
        {
            get { return 3137; }
        }

        internal override string Name
        {
            get { return "Dervish"; }
        }

        internal override string DisplayName
        {
            get { return "Dervish Blade"; }
        }

        internal override int Cooldown
        {
            get { return 90000; }
        }

        internal override float Range
        {
            get { return float.MaxValue; }
        }

        internal override MenuType[] Category
        {
            get { return new[] { MenuType.Cleanse, MenuType.ActiveCheck }; }
        }

        internal override MapType[] Maps
        {
            get { return new[] { MapType.CrystalScar, MapType.TwistedTreeline }; }
        }

        internal override int DefaultHP
        {
            get { return 5; }
        }

        internal override int DefaultMP
        {
            get { return 0; }
        }

        public override void OnTick(EventArgs args)
        {
            foreach (var hero in champion.Heroes)
            {
                if (!Menu.Item("use" + Name).GetValue<bool>())
                    return;

                if (hero.Player.NetworkId == Player.NetworkId)
                {
                    if (hero.Player.Distance(Player.ServerPosition) > Range)
                        return;

                    if (hero.ForceQSS)
                    {
                        UseItem();
                        hero.IncomeDamage = 0;
                    }

                    if (hero.QSSBuffCount >= Menu.Item("use" + Name + "Number").GetValue<Slider>().Value)
                    {
                        if (!Menu.Item("use" + Name + "Od").GetValue<bool>())
                        {
                            Utility.DelayAction.Add(Game.Ping + 150, delegate
                            {
                                UseItem(Menu.Item("mode" + Name).GetValue<StringList>().SelectedIndex == 1);
                            });
                        }
                    }
                }
            }
        }
    }
}
