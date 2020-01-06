// RemoteComObjectImpl.cpp : Implementation of CRemoteComObjectImpl

#include "stdafx.h"
#include "RemoteComObjectImpl.h"


// CRemoteComObjectImpl



STDMETHODIMP CRemoteComObjectImpl::get_Property(BSTR* pVal)
{
	*pVal = SysAllocString(m_propertyValue.c_str());
	return S_OK;
}


STDMETHODIMP CRemoteComObjectImpl::put_Property(BSTR newVal)
{
	m_propertyValue = newVal;
	return S_OK;
}


STDMETHODIMP CRemoteComObjectImpl::MethodWithParametersAndReturnValue(BSTR stringParameter, INT integerParameter, BSTR* stringResult)
{
	std::wstring result = L"MethodWithParametersAndReturnValue(";
	result += stringParameter;
	result += L", ";
	result += std::to_wstring(integerParameter);
	result += L") called.";
	*stringResult = SysAllocString(result.c_str());
	return S_OK;
}


STDMETHODIMP CRemoteComObjectImpl::CallCallbackAsynchronously(IDispatch* callbackParameter)
{
	wil::com_ptr<IDispatch> callbackParameterForCapture = callbackParameter;

	auto release = std::async(std::launch::async, [callbackParameterForCapture] {
		callbackParameterForCapture->Invoke(
			DISPID_UNKNOWN, IID_NULL, LOCALE_USER_DEFAULT, DISPATCH_METHOD, nullptr, nullptr,
			nullptr, nullptr);
	});

	return S_OK;
}
