using System;


namespace R5T.S0044
{
	public class LibraryOperations : ILibraryOperations
	{
		#region Infrastructure

	    public static ILibraryOperations Instance { get; } = new LibraryOperations();

	    private LibraryOperations()
	    {
        }

	    #endregion
	}
}