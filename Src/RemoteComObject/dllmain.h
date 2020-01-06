// dllmain.h : Declaration of module class.

class CRemoteComObjectModule : public ATL::CAtlDllModuleT< CRemoteComObjectModule >
{
public :
	DECLARE_LIBID(LIBID_RemoteComObjectLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_REMOTECOMOBJECT, "{55589A99-0146-4AA4-BEB2-5013185E5772}")
};

extern class CRemoteComObjectModule _AtlModule;
