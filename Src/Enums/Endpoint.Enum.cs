namespace BaseCodeAPI.Src.Enums
{
   public enum eEndpoint : byte
   {
      MigrationApply,
      MigrationRevert,

      AuthenticateLogin,
      AuthenticateRegister,

      Users,
      UserById,
      UserAdd,
      UserEdit,
      UserRemove,

      Clients,
      ClientById,
      ClientAdd,
      ClientEdit,
      ClientRemove,

      Address,
      AddressById,
      AddressAdd,
      AddressEdit,
      AddressRemove,
   }
}
