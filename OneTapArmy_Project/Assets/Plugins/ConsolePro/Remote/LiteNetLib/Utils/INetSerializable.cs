namespace Plugins.ConsolePro.Remote.LiteNetLib.Utils
{
    public interface INetSerializable
    {
        void Serialize(NetDataWriter writer);
        void Deserialize(NetDataReader reader);
    }
}
