#pragma once
#pragma comment(lib, "winhttp.lib")

#include <string>
#include <Windows.h>
#include <map>
#include <winhttp.h>

using std::string;
using std::map;
using std::wstring;

struct HttpResult
{
	DWORD code;
	string body;
	bool requestSent;
};

class HttpSystem
{
private:
	wstring UTF8ToUTF16(const string& in_utf8String);
	wstring CreateHeaderString(const map<string, string>& headers);
	BOOL GetResponseData(const HINTERNET hRequest, string & responseData, DWORD & dwStatusCode);

public:
	HttpSystem();
	~HttpSystem();
	HttpResult MakePostRequest(const string& host, const string& path, const map<string,string>&headers, const string &body);
};

