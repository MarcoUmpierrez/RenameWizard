namespace RenameWizard.Handlers
{
    public interface IRenameHandler
    {
        void SetHandler(IRenameHandler handler);
        string Rename(string name, string ext);
    }
}
