using Unity.Collections;
using Unity.NetCode;

namespace LefterisRentas.Dev.Helpers.Items.Entities.Netcode.Common.Contracts
{
    public struct ClientItemRequest : IRpcCommand
    {
        public FixedString32Bytes ItemId;
    }
}