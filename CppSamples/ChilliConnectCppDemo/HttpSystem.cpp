#include "HttpSystem.h"

#include <iostream>
#include <string>
#include <Windows.h>
#include <winhttp.h>
#include <codecvt>
#include <locale>

using std::string;
using std::map;
using std::wstring;

wstring
HttpSystem::CreateHeaderString(const map<string,string>& headers)
{
	string headerBlob = "Content-Type: application/json\r\n";

	for (auto it = headers.begin(); it != headers.end(); ++it)
	{
		headerBlob += it->first;
		headerBlob += ": ";
		headerBlob += it->second;
		headerBlob += '\r';
		headerBlob += '\n';
	}

	wstring headerBlobWide(headerBlob.length() - 2, L' ');
	std::copy(headerBlob.begin(), headerBlob.end() - 2, headerBlobWide.begin());
	return headerBlobWide;
}

wstring
HttpSystem::UTF8ToUTF16(const string &utf8String)
{
	std::wstring_convert<std::codecvt_utf8_utf16<wchar_t>> converter;
	return converter.from_bytes(utf8String);
}

HttpResult
HttpSystem::MakePostRequest(const string &host, const string &path, const map<string, string>&headers, const string &body)
{
	HttpResult httpResult;
	httpResult.code = -1;
	httpResult.body = "";
	httpResult.requestSent = false;

	HINTERNET hSession = NULL;
	HINTERNET hConnect = NULL;
	HINTERNET hRequest = NULL;

	wstring wHost(UTF8ToUTF16(host));
	wstring wPath(UTF8ToUTF16(path));

	hSession = WinHttpOpen(L"WinHTTP Example/1.0", WINHTTP_ACCESS_TYPE_DEFAULT_PROXY, WINHTTP_NO_PROXY_NAME, WINHTTP_NO_PROXY_BYPASS, 0);
	if (!hSession) {
		return httpResult;
	}

	hConnect = WinHttpConnect(hSession, wHost.c_str(), INTERNET_DEFAULT_HTTPS_PORT, 0);
	if (!hConnect) {
		WinHttpCloseHandle(hSession);
		return httpResult;
	}

	hRequest = WinHttpOpenRequest(hConnect, L"POST", wPath.c_str(), NULL, WINHTTP_NO_REFERER,WINHTTP_DEFAULT_ACCEPT_TYPES, WINHTTP_FLAG_SECURE);
	if (!hRequest) {
		WinHttpCloseHandle(hConnect);
		WinHttpCloseHandle(hSession);
		return httpResult;
	}

	if (headers.empty() == false) {
		std::wstring headerBlob = CreateHeaderString(headers);
		if (!WinHttpAddRequestHeaders(hRequest, headerBlob.c_str(), DWORD(headerBlob.length()), WINHTTP_ADDREQ_FLAG_ADD)) {
			WinHttpCloseHandle(hConnect);
			WinHttpCloseHandle(hSession);
			return httpResult;
		}
	}

	if (!WinHttpSendRequest(hRequest, WINHTTP_NO_ADDITIONAL_HEADERS, 0, (LPVOID)body.data(), DWORD(body.length()), DWORD(body.length()), 0)) {
		WinHttpCloseHandle(hConnect);
		WinHttpCloseHandle(hSession);
		return httpResult;
	}
	
	string responseData;
	DWORD statusCode = 0;
	if (GetResponseData(hRequest, responseData, statusCode)) {
		httpResult.code = statusCode;
		httpResult.body = responseData;
		httpResult.requestSent = true;
	}

	if (hRequest) WinHttpCloseHandle(hRequest);
	if (hConnect) WinHttpCloseHandle(hConnect);
	if (hSession) WinHttpCloseHandle(hSession);

	return httpResult;
}

BOOL 
HttpSystem::GetResponseData(const HINTERNET hRequest, string & responseData, DWORD & statusCode) {
	if (!WinHttpReceiveResponse(hRequest, NULL)) {
		return false;
	}

	DWORD dwSize = sizeof(statusCode);
	if (!WinHttpQueryHeaders(hRequest, WINHTTP_QUERY_STATUS_CODE | WINHTTP_QUERY_FLAG_NUMBER, WINHTTP_HEADER_NAME_BY_INDEX, &statusCode, &dwSize, WINHTTP_NO_HEADER_INDEX)) {
		return false;
	}

	LPSTR pszOutBuffer;
	DWORD dwDownloaded = 0;
	do {
		dwSize = 0;
		if (!WinHttpQueryDataAvailable(hRequest, &dwSize)) {
			return false;
		}

		if (!dwSize)
			break;

		pszOutBuffer = new char[dwSize + 1];
		if (!pszOutBuffer) {
			return false;
			break;
		}

		ZeroMemory(pszOutBuffer, dwSize + 1);
		if (!WinHttpReadData(hRequest, (LPVOID)pszOutBuffer, dwSize, &dwDownloaded)) {
			delete[] pszOutBuffer;
			return false;
		}
		else {
			responseData.append(pszOutBuffer);
		}

		delete[] pszOutBuffer;

		if (!dwDownloaded)
			break;

	} while (dwSize > 0);
	
	return true;
}

