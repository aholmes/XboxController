#pragma once

// Windows build
#if defined (_WIN32)
#if defined (ADAPTERS_API_EXPORTS)
#define ADAPTERS_API __declspec(dllexport)
#else
#define ADAPTERS_API __declspec(dllimport)
#endif // SAMPLELIBRARY_DLL_EXPORTS
#endif // _WIN32

// Apple build
#if defined(__APPLE__)
#define ADAPTERS_API __attribute__ ((visibility ("default")))
#endif // __APPLE__




static BOOL ReadDeviceName(int index, HANDLE handle);
static BOOL ReadDeviceInfo(int index, HANDLE handle);
static int FindIndex(HANDLE handle);
static void ParseRawInput(PRAWINPUT pRawInput, unsigned char * buttons, int * x, int * y);

ADAPTERS_API int InitialiseGamepads(HWND handle);
ADAPTERS_API TCHAR * GetDevicePath(int index);
ADAPTERS_API int PollDeviceChange();
ADAPTERS_API int ProcessInput(HANDLE wParam, HANDLE lParam, unsigned char * buttons, int * x, int * y);
