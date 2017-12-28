using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot
{
    [global::ProtoBuf.ProtoContract(Name = @"CMsgClientToGCMatchesMinimalRequest")]
    public partial class CMsgClientToGCMatchesMinimalRequest : global::ProtoBuf.IExtensible
    {
        public CMsgClientToGCMatchesMinimalRequest() { }

        private global::System.Collections.Generic.List<ulong> _match_ids = new global::System.Collections.Generic.List<ulong>();
        [global::ProtoBuf.ProtoMember(1, Name = @"match_ids", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
        public global::System.Collections.Generic.List<ulong> match_ids
        {
            get { return _match_ids; }
            set { _match_ids = value; }
        }

        private global::ProtoBuf.IExtension extensionObject;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
        { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }
    [global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"CMsgDOTAProfileCard")]
    public partial class CMsgDOTAProfileCard : global::ProtoBuf.IExtensible
    {
        public CMsgDOTAProfileCard() { }


        private uint? _account_id;
        [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name = @"account_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
        public uint account_id
        {
            get { return _account_id ?? default(uint); }
            set { _account_id = value; }
        }
        [global::System.Xml.Serialization.XmlIgnore]
        [global::System.ComponentModel.Browsable(false)]
        public bool account_idSpecified
        {
            get { return _account_id != null; }
            set { if (value == (_account_id == null)) _account_id = value ? this.account_id : (uint?)null; }
        }
        private bool ShouldSerializeaccount_id() { return account_idSpecified; }
        private void Resetaccount_id() { account_idSpecified = false; }


        private uint? _background_def_index;
        [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name = @"background_def_index", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
        public uint background_def_index
        {
            get { return _background_def_index ?? default(uint); }
            set { _background_def_index = value; }
        }
        [global::System.Xml.Serialization.XmlIgnore]
        [global::System.ComponentModel.Browsable(false)]
        public bool background_def_indexSpecified
        {
            get { return _background_def_index != null; }
            set { if (value == (_background_def_index == null)) _background_def_index = value ? this.background_def_index : (uint?)null; }
        }
        private bool ShouldSerializebackground_def_index() { return background_def_indexSpecified; }
        private void Resetbackground_def_index() { background_def_indexSpecified = false; }

        private readonly global::System.Collections.Generic.List<CMsgDOTAProfileCard.Slot> _slots = new global::System.Collections.Generic.List<CMsgDOTAProfileCard.Slot>();
        [global::ProtoBuf.ProtoMember(3, Name = @"slots", DataFormat = global::ProtoBuf.DataFormat.Default)]
        public global::System.Collections.Generic.List<CMsgDOTAProfileCard.Slot> slots
        {
            get { return _slots; }
        }


        private uint? _badge_pointss;
        [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name = @"badge_points", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
        public uint badge_points
        {
            get { return _badge_pointss ?? default(uint); }
            set { _badge_pointss = value; }
        }
        [global::System.Xml.Serialization.XmlIgnore]
        [global::System.ComponentModel.Browsable(false)]
        public bool badge_pointsSpecified
        {
            get { return _badge_pointss != null; }
            set { if (value == (_badge_pointss == null)) _badge_pointss = value ? this.badge_points : (uint?)null; }
        }
        private bool ShouldSerializebadge_points() { return badge_pointsSpecified; }
        private void Resetbadge_points() { badge_pointsSpecified = false; }


        private uint? _event_points;
        [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name = @"event_points", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
        public uint event_points
        {
            get { return _event_points ?? default(uint); }
            set { _event_points = value; }
        }
        [global::System.Xml.Serialization.XmlIgnore]
        [global::System.ComponentModel.Browsable(false)]
        public bool event_pointsSpecified
        {
            get { return _event_points != null; }
            set { if (value == (_event_points == null)) _event_points = value ? this.event_points : (uint?)null; }
        }
        private bool ShouldSerializeevent_points() { return event_pointsSpecified; }
        private void Resetevent_points() { event_pointsSpecified = false; }


        private uint? _event_id;
        [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name = @"event_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
        public uint event_id
        {
            get { return _event_id ?? default(uint); }
            set { _event_id = value; }
        }
        [global::System.Xml.Serialization.XmlIgnore]
        [global::System.ComponentModel.Browsable(false)]
        public bool event_idSpecified
        {
            get { return _event_id != null; }
            set { if (value == (_event_id == null)) _event_id = value ? this.event_id : (uint?)null; }
        }
        private bool ShouldSerializeevent_id() { return event_idSpecified; }
        private void Resetevent_id() { event_idSpecified = false; }


        private CMsgBattleCupVictory _recent_battle_cup_victory = null;
        [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name = @"recent_battle_cup_victory", DataFormat = global::ProtoBuf.DataFormat.Default)]
        [global::System.ComponentModel.DefaultValue(null)]
        public CMsgBattleCupVictory recent_battle_cup_victory
        {
            get { return _recent_battle_cup_victory; }
            set { _recent_battle_cup_victory = value; }
        }
        private uint? _rank_tier;
        [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name = @"rank_tier", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
        public uint rank_tier
        {
            get { return _rank_tier ?? default(uint); }
            set { _rank_tier = value; }
        }
        private uint? _leaderboard_position;
        [global::ProtoBuf.ProtoMember(9, IsRequired = false, Name = @"leaderboard_position", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
        public uint leaderboard_position
        {
            get { return _leaderboard_position ?? default(uint); }
            set { _leaderboard_position = value; }
        }
        [global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"Slot")]
        public partial class Slot : global::ProtoBuf.IExtensible
        {
            public Slot() { }


            private uint? _slot_id;
            [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name = @"slot_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
            public uint slot_id
            {
                get { return _slot_id ?? default(uint); }
                set { _slot_id = value; }
            }
            [global::System.Xml.Serialization.XmlIgnore]
            [global::System.ComponentModel.Browsable(false)]
            public bool slot_idSpecified
            {
                get { return _slot_id != null; }
                set { if (value == (_slot_id == null)) _slot_id = value ? this.slot_id : (uint?)null; }
            }
            private bool ShouldSerializeslot_id() { return slot_idSpecified; }
            private void Resetslot_id() { slot_idSpecified = false; }


            private CMsgDOTAProfileCard.Slot.Trophy _trophy = null;
            [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name = @"trophy", DataFormat = global::ProtoBuf.DataFormat.Default)]
            [global::System.ComponentModel.DefaultValue(null)]
            public CMsgDOTAProfileCard.Slot.Trophy trophy
            {
                get { return _trophy; }
                set { _trophy = value; }
            }

            private CMsgDOTAProfileCard.Slot.Stat _stat = null;
            [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name = @"stat", DataFormat = global::ProtoBuf.DataFormat.Default)]
            [global::System.ComponentModel.DefaultValue(null)]
            public CMsgDOTAProfileCard.Slot.Stat stat
            {
                get { return _stat; }
                set { _stat = value; }
            }

            private CMsgDOTAProfileCard.Slot.Item _item = null;
            [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name = @"item", DataFormat = global::ProtoBuf.DataFormat.Default)]
            [global::System.ComponentModel.DefaultValue(null)]
            public CMsgDOTAProfileCard.Slot.Item item
            {
                get { return _item; }
                set { _item = value; }
            }

            private CMsgDOTAProfileCard.Slot.Hero _hero = null;
            [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name = @"hero", DataFormat = global::ProtoBuf.DataFormat.Default)]
            [global::System.ComponentModel.DefaultValue(null)]
            public CMsgDOTAProfileCard.Slot.Hero hero
            {
                get { return _hero; }
                set { _hero = value; }
            }

            private CMsgDOTAProfileCard.Slot.Emoticon _emoticon = null;
            [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name = @"emoticon", DataFormat = global::ProtoBuf.DataFormat.Default)]
            [global::System.ComponentModel.DefaultValue(null)]
            public CMsgDOTAProfileCard.Slot.Emoticon emoticon
            {
                get { return _emoticon; }
                set { _emoticon = value; }
            }

            private CMsgDOTAProfileCard.Slot.Team _team = null;
            [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name = @"team", DataFormat = global::ProtoBuf.DataFormat.Default)]
            [global::System.ComponentModel.DefaultValue(null)]
            public CMsgDOTAProfileCard.Slot.Team team
            {
                get { return _team; }
                set { _team = value; }
            }
            [global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"Trophy")]
            public partial class Trophy : global::ProtoBuf.IExtensible
            {
                public Trophy() { }


                private uint? _trophy_id;
                [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name = @"trophy_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
                public uint trophy_id
                {
                    get { return _trophy_id ?? default(uint); }
                    set { _trophy_id = value; }
                }
                [global::System.Xml.Serialization.XmlIgnore]
                [global::System.ComponentModel.Browsable(false)]
                public bool trophy_idSpecified
                {
                    get { return _trophy_id != null; }
                    set { if (value == (_trophy_id == null)) _trophy_id = value ? this.trophy_id : (uint?)null; }
                }
                private bool ShouldSerializetrophy_id() { return trophy_idSpecified; }
                private void Resettrophy_id() { trophy_idSpecified = false; }


                private uint? _trophy_score;
                [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name = @"trophy_score", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
                public uint trophy_score
                {
                    get { return _trophy_score ?? default(uint); }
                    set { _trophy_score = value; }
                }
                [global::System.Xml.Serialization.XmlIgnore]
                [global::System.ComponentModel.Browsable(false)]
                public bool trophy_scoreSpecified
                {
                    get { return _trophy_score != null; }
                    set { if (value == (_trophy_score == null)) _trophy_score = value ? this.trophy_score : (uint?)null; }
                }
                private bool ShouldSerializetrophy_score() { return trophy_scoreSpecified; }
                private void Resettrophy_score() { trophy_scoreSpecified = false; }

                private global::ProtoBuf.IExtension extensionObject;
                global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
            }

            [global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"Stat")]
            public partial class Stat : global::ProtoBuf.IExtensible
            {
                public Stat() { }


                private CMsgDOTAProfileCard.EStatID? _stat_id;
                [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name = @"stat_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
                public CMsgDOTAProfileCard.EStatID stat_id
                {
                    get { return _stat_id ?? CMsgDOTAProfileCard.EStatID.k_eStat_SoloRank; }
                    set { _stat_id = value; }
                }
                [global::System.Xml.Serialization.XmlIgnore]
                [global::System.ComponentModel.Browsable(false)]
                public bool stat_idSpecified
                {
                    get { return _stat_id != null; }
                    set { if (value == (_stat_id == null)) _stat_id = value ? this.stat_id : (CMsgDOTAProfileCard.EStatID?)null; }
                }
                private bool ShouldSerializestat_id() { return stat_idSpecified; }
                private void Resetstat_id() { stat_idSpecified = false; }


                private uint? _stat_score;
                [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name = @"stat_score", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
                public uint stat_score
                {
                    get { return _stat_score ?? default(uint); }
                    set { _stat_score = value; }
                }
                [global::System.Xml.Serialization.XmlIgnore]
                [global::System.ComponentModel.Browsable(false)]
                public bool stat_scoreSpecified
                {
                    get { return _stat_score != null; }
                    set { if (value == (_stat_score == null)) _stat_score = value ? this.stat_score : (uint?)null; }
                }
                private bool ShouldSerializestat_score() { return stat_scoreSpecified; }
                private void Resetstat_score() { stat_scoreSpecified = false; }

                private global::ProtoBuf.IExtension extensionObject;
                global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
            }

            [global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"Item")]
            public partial class Item : global::ProtoBuf.IExtensible
            {
                public Item() { }


                private byte[] _serialized_item;
                [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name = @"serialized_item", DataFormat = global::ProtoBuf.DataFormat.Default)]
                public byte[] serialized_item
                {
                    get { return _serialized_item ?? null; }
                    set { _serialized_item = value; }
                }
                [global::System.Xml.Serialization.XmlIgnore]
                [global::System.ComponentModel.Browsable(false)]
                public bool serialized_itemSpecified
                {
                    get { return _serialized_item != null; }
                    set { if (value == (_serialized_item == null)) _serialized_item = value ? this.serialized_item : (byte[])null; }
                }
                private bool ShouldSerializeserialized_item() { return serialized_itemSpecified; }
                private void Resetserialized_item() { serialized_itemSpecified = false; }


                private ulong? _item_id;
                [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name = @"item_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
                public ulong item_id
                {
                    get { return _item_id ?? default(ulong); }
                    set { _item_id = value; }
                }
                [global::System.Xml.Serialization.XmlIgnore]
                [global::System.ComponentModel.Browsable(false)]
                public bool item_idSpecified
                {
                    get { return _item_id != null; }
                    set { if (value == (_item_id == null)) _item_id = value ? this.item_id : (ulong?)null; }
                }
                private bool ShouldSerializeitem_id() { return item_idSpecified; }
                private void Resetitem_id() { item_idSpecified = false; }

                private global::ProtoBuf.IExtension extensionObject;
                global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
            }

            [global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"Hero")]
            public partial class Hero : global::ProtoBuf.IExtensible
            {
                public Hero() { }


                private uint? _hero_id;
                [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name = @"hero_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
                public uint hero_id
                {
                    get { return _hero_id ?? default(uint); }
                    set { _hero_id = value; }
                }
                [global::System.Xml.Serialization.XmlIgnore]
                [global::System.ComponentModel.Browsable(false)]
                public bool hero_idSpecified
                {
                    get { return _hero_id != null; }
                    set { if (value == (_hero_id == null)) _hero_id = value ? this.hero_id : (uint?)null; }
                }
                private bool ShouldSerializehero_id() { return hero_idSpecified; }
                private void Resethero_id() { hero_idSpecified = false; }


                private uint? _hero_wins;
                [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name = @"hero_wins", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
                public uint hero_wins
                {
                    get { return _hero_wins ?? default(uint); }
                    set { _hero_wins = value; }
                }
                [global::System.Xml.Serialization.XmlIgnore]
                [global::System.ComponentModel.Browsable(false)]
                public bool hero_winsSpecified
                {
                    get { return _hero_wins != null; }
                    set { if (value == (_hero_wins == null)) _hero_wins = value ? this.hero_wins : (uint?)null; }
                }
                private bool ShouldSerializehero_wins() { return hero_winsSpecified; }
                private void Resethero_wins() { hero_winsSpecified = false; }


                private uint? _hero_losses;
                [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name = @"hero_losses", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
                public uint hero_losses
                {
                    get { return _hero_losses ?? default(uint); }
                    set { _hero_losses = value; }
                }
                [global::System.Xml.Serialization.XmlIgnore]
                [global::System.ComponentModel.Browsable(false)]
                public bool hero_lossesSpecified
                {
                    get { return _hero_losses != null; }
                    set { if (value == (_hero_losses == null)) _hero_losses = value ? this.hero_losses : (uint?)null; }
                }
                private bool ShouldSerializehero_losses() { return hero_lossesSpecified; }
                private void Resethero_losses() { hero_lossesSpecified = false; }

                private global::ProtoBuf.IExtension extensionObject;
                global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
            }

            [global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"Emoticon")]
            public partial class Emoticon : global::ProtoBuf.IExtensible
            {
                public Emoticon() { }


                private uint? _emoticon_id;
                [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name = @"emoticon_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
                public uint emoticon_id
                {
                    get { return _emoticon_id ?? default(uint); }
                    set { _emoticon_id = value; }
                }
                [global::System.Xml.Serialization.XmlIgnore]
                [global::System.ComponentModel.Browsable(false)]
                public bool emoticon_idSpecified
                {
                    get { return _emoticon_id != null; }
                    set { if (value == (_emoticon_id == null)) _emoticon_id = value ? this.emoticon_id : (uint?)null; }
                }
                private bool ShouldSerializeemoticon_id() { return emoticon_idSpecified; }
                private void Resetemoticon_id() { emoticon_idSpecified = false; }

                private global::ProtoBuf.IExtension extensionObject;
                global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
            }

            [global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"Team")]
            public partial class Team : global::ProtoBuf.IExtensible
            {
                public Team() { }


                private uint? _team_id;
                [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name = @"team_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
                public uint team_id
                {
                    get { return _team_id ?? default(uint); }
                    set { _team_id = value; }
                }
                [global::System.Xml.Serialization.XmlIgnore]
                [global::System.ComponentModel.Browsable(false)]
                public bool team_idSpecified
                {
                    get { return _team_id != null; }
                    set { if (value == (_team_id == null)) _team_id = value ? this.team_id : (uint?)null; }
                }
                private bool ShouldSerializeteam_id() { return team_idSpecified; }
                private void Resetteam_id() { team_idSpecified = false; }

                private global::ProtoBuf.IExtension extensionObject;
                global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
            }

            private global::ProtoBuf.IExtension extensionObject;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
        }

        [global::ProtoBuf.ProtoContract(Name = @"EStatID", EnumPassthru = true)]
        public enum EStatID
        {

            [global::ProtoBuf.ProtoEnum(Name = @"k_eStat_SoloRank", Value = 1)]
            k_eStat_SoloRank = 1,

            [global::ProtoBuf.ProtoEnum(Name = @"k_eStat_PartyRank", Value = 2)]
            k_eStat_PartyRank = 2,

            [global::ProtoBuf.ProtoEnum(Name = @"k_eStat_Wins", Value = 3)]
            k_eStat_Wins = 3,

            [global::ProtoBuf.ProtoEnum(Name = @"k_eStat_Commends", Value = 4)]
            k_eStat_Commends = 4,

            [global::ProtoBuf.ProtoEnum(Name = @"k_eStat_GamesPlayed", Value = 5)]
            k_eStat_GamesPlayed = 5,

            [global::ProtoBuf.ProtoEnum(Name = @"k_eStat_FirstMatchDate", Value = 6)]
            k_eStat_FirstMatchDate = 6
        }

        private global::ProtoBuf.IExtension extensionObject;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
        { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }
    [global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"CMsgBattleCupVictory")]
    public partial class CMsgBattleCupVictory : global::ProtoBuf.IExtensible
    {
        public CMsgBattleCupVictory() { }


        private uint? _account_id;
        [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name = @"account_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
        public uint account_id
        {
            get { return _account_id ?? default(uint); }
            set { _account_id = value; }
        }
        [global::System.Xml.Serialization.XmlIgnore]
        [global::System.ComponentModel.Browsable(false)]
        public bool account_idSpecified
        {
            get { return _account_id != null; }
            set { if (value == (_account_id == null)) _account_id = value ? this.account_id : (uint?)null; }
        }
        private bool ShouldSerializeaccount_id() { return account_idSpecified; }
        private void Resetaccount_id() { account_idSpecified = false; }


        private uint? _win_date;
        [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name = @"win_date", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
        public uint win_date
        {
            get { return _win_date ?? default(uint); }
            set { _win_date = value; }
        }
        [global::System.Xml.Serialization.XmlIgnore]
        [global::System.ComponentModel.Browsable(false)]
        public bool win_dateSpecified
        {
            get { return _win_date != null; }
            set { if (value == (_win_date == null)) _win_date = value ? this.win_date : (uint?)null; }
        }
        private bool ShouldSerializewin_date() { return win_dateSpecified; }
        private void Resetwin_date() { win_dateSpecified = false; }


        private uint? _valid_until;
        [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name = @"valid_until", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
        public uint valid_until
        {
            get { return _valid_until ?? default(uint); }
            set { _valid_until = value; }
        }
        [global::System.Xml.Serialization.XmlIgnore]
        [global::System.ComponentModel.Browsable(false)]
        public bool valid_untilSpecified
        {
            get { return _valid_until != null; }
            set { if (value == (_valid_until == null)) _valid_until = value ? this.valid_until : (uint?)null; }
        }
        private bool ShouldSerializevalid_until() { return valid_untilSpecified; }
        private void Resetvalid_until() { valid_untilSpecified = false; }


        private uint? _skill_level;
        [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name = @"skill_level", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
        public uint skill_level
        {
            get { return _skill_level ?? default(uint); }
            set { _skill_level = value; }
        }
        [global::System.Xml.Serialization.XmlIgnore]
        [global::System.ComponentModel.Browsable(false)]
        public bool skill_levelSpecified
        {
            get { return _skill_level != null; }
            set { if (value == (_skill_level == null)) _skill_level = value ? this.skill_level : (uint?)null; }
        }
        private bool ShouldSerializeskill_level() { return skill_levelSpecified; }
        private void Resetskill_level() { skill_levelSpecified = false; }


        private uint? _tournament_id;
        [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name = @"tournament_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
        public uint tournament_id
        {
            get { return _tournament_id ?? default(uint); }
            set { _tournament_id = value; }
        }
        [global::System.Xml.Serialization.XmlIgnore]
        [global::System.ComponentModel.Browsable(false)]
        public bool tournament_idSpecified
        {
            get { return _tournament_id != null; }
            set { if (value == (_tournament_id == null)) _tournament_id = value ? this.tournament_id : (uint?)null; }
        }
        private bool ShouldSerializetournament_id() { return tournament_idSpecified; }
        private void Resettournament_id() { tournament_idSpecified = false; }


        private uint? _division_id;
        [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name = @"division_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
        public uint division_id
        {
            get { return _division_id ?? default(uint); }
            set { _division_id = value; }
        }
        [global::System.Xml.Serialization.XmlIgnore]
        [global::System.ComponentModel.Browsable(false)]
        public bool division_idSpecified
        {
            get { return _division_id != null; }
            set { if (value == (_division_id == null)) _division_id = value ? this.division_id : (uint?)null; }
        }
        private bool ShouldSerializedivision_id() { return division_idSpecified; }
        private void Resetdivision_id() { division_idSpecified = false; }


        private uint? _team_id;
        [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name = @"team_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
        public uint team_id
        {
            get { return _team_id ?? default(uint); }
            set { _team_id = value; }
        }
        [global::System.Xml.Serialization.XmlIgnore]
        [global::System.ComponentModel.Browsable(false)]
        public bool team_idSpecified
        {
            get { return _team_id != null; }
            set { if (value == (_team_id == null)) _team_id = value ? this.team_id : (uint?)null; }
        }
        private bool ShouldSerializeteam_id() { return team_idSpecified; }
        private void Resetteam_id() { team_idSpecified = false; }


        private uint? _streak;
        [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name = @"streak", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
        public uint streak
        {
            get { return _streak ?? default(uint); }
            set { _streak = value; }
        }
        [global::System.Xml.Serialization.XmlIgnore]
        [global::System.ComponentModel.Browsable(false)]
        public bool streakSpecified
        {
            get { return _streak != null; }
            set { if (value == (_streak == null)) _streak = value ? this.streak : (uint?)null; }
        }
        private bool ShouldSerializestreak() { return streakSpecified; }
        private void Resetstreak() { streakSpecified = false; }


        private uint? _trophy_id;
        [global::ProtoBuf.ProtoMember(9, IsRequired = false, Name = @"trophy_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
        public uint trophy_id
        {
            get { return _trophy_id ?? default(uint); }
            set { _trophy_id = value; }
        }
        [global::System.Xml.Serialization.XmlIgnore]
        [global::System.ComponentModel.Browsable(false)]
        public bool trophy_idSpecified
        {
            get { return _trophy_id != null; }
            set { if (value == (_trophy_id == null)) _trophy_id = value ? this.trophy_id : (uint?)null; }
        }
        private bool ShouldSerializetrophy_id() { return trophy_idSpecified; }
        private void Resettrophy_id() { trophy_idSpecified = false; }

        private global::ProtoBuf.IExtension extensionObject;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
        { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }
}
