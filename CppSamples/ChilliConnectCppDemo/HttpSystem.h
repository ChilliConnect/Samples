#pragma once
#pragma comment(lib, "winhttp.lib")

#include <string>
#include <Windows.h>
#include <map>
#include <winhttp.h>

struct HttpResult
{
	DWORD code;
	std::string body;
	bool requestSent;
};

class HttpSystem
{
private:
	std::wstring UTF8ToUTF16(const std::string& in_utf8String);
	std::wstring CreateHeaderString(const std::map<std::string, std::string>& headers);
	BOOL GetResponseData(const HINTERNET hRequest, std::string & responseData, DWORD & dwStatusCode);

public:
	HttpResult MakePostRequest(const std::string& host, const std::string& path, const std::map<std::string, std::string>&headers, const std::string &body);
};

