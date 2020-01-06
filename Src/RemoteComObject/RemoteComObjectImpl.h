// RemoteComObjectImpl.h : Declaration of the CRemoteComObjectImpl

#pragma once
#include "resource.h"       // main symbols

#include <functional>
#include <string>
#include <wrl\client.h>

#include "RemoteComObject_i.h"



#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Single-threaded COM objects are not properly supported on Windows CE platform, such as the Windows Mobile platforms that do not include full DCOM support. Define _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA to force ATL to support creating single-thread COM object's and allow use of it's single-threaded COM object implementations. The threading model in your rgs file was set to 'Free' as that is the only threading model supported in non DCOM Windows CE platforms."
#endif

using namespace ATL;


// CRemoteComObjectImpl

class ATL_NO_VTABLE CRemoteComObjectImpl :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CRemoteComObjectImpl, &CLSID_RemoteComObjectImpl>,
	public IDispatchImpl<IRemoteComObjectImpl, &IID_IRemoteComObjectImpl, &LIBID_RemoteComObjectLib, /*wMajor =*/ 1, /*wMinor =*/ 0>
{
public:
	CRemoteComObjectImpl()
	{
		m_propertyValue = L"Example Property String Value";
	}

DECLARE_REGISTRY_RESOURCEID(IDR_REMOTECOMOBJECTIMPL)


BEGIN_COM_MAP(CRemoteComObjectImpl)
	COM_INTERFACE_ENTRY(IRemoteComObjectImpl)
	COM_INTERFACE_ENTRY(IDispatch)
END_COM_MAP()



	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}

	void FinalRelease()
	{
	}

public:



	STDMETHOD(get_Property)(BSTR* pVal);
	STDMETHOD(put_Property)(BSTR newVal);
	STDMETHOD(MethodWithParametersAndReturnValue)(BSTR stringParameter, INT integerParameter, BSTR* stringResult);
	STDMETHOD(CallCallbackAsynchronously)(IDispatch* callbackParameter);

private:
	std::wstring m_propertyValue;
	wil::com_ptr<IDispatch> m_callback;
	wil::com_ptr<ITypeLib> m_typeLib;
};

OBJECT_ENTRY_AUTO(__uuidof(RemoteComObjectImpl), CRemoteComObjectImpl)
