

[global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"CMsgGCPlayerInfoRequest")]
public partial class CMsgGCPlayerInfoRequest : global::ProtoBuf.IExtensible
{
    public CMsgGCPlayerInfoRequest() { }

    private global::System.Collections.Generic.List<CMsgGCPlayerInfoRequest.PlayerInfo> _player_infos = new global::System.Collections.Generic.List<CMsgGCPlayerInfoRequest.PlayerInfo>();
    [global::ProtoBuf.ProtoMember(1, Name = @"player_infos", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<CMsgGCPlayerInfoRequest.PlayerInfo> player_infos
    {
        get { return _player_infos; }
        set { _player_infos = value; }
    }

    [global::System.Serializable, global::ProtoBuf.ProtoContract(Name = @"PlayerInfo")]
    public partial class PlayerInfo : global::ProtoBuf.IExtensible
    {
        public PlayerInfo() { }


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


        private uint? _timestamp;
        [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name = @"timestamp", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
        public uint timestamp
        {
            get { return _timestamp ?? default(uint); }
            set { _timestamp = value; }
        }
        [global::System.Xml.Serialization.XmlIgnore]
        [global::System.ComponentModel.Browsable(false)]
        public bool timestampSpecified
        {
            get { return _timestamp != null; }
            set { if (value == (_timestamp == null)) _timestamp = value ? this.timestamp : (uint?)null; }
        }
        private bool ShouldSerializetimestamp() { return timestampSpecified; }
        private void Resettimestamp() { timestampSpecified = false; }

        private global::ProtoBuf.IExtension extensionObject;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
        { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
    }

    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
    { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
}