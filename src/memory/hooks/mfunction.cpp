/************************************************************************************************
 *  SwiftlyS2 is a scripting framework for Source2-based games.
 *  Copyright (C) 2025 Swiftly Solution SRL via Sava Andrei-Sebastian and it's contributors
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
 ************************************************************************************************/

#include "mfunction.h"
#include <Windows.h>
#include <cstdio>

MFunctionHook* MFunctionHook::s_currentInstance = nullptr;

static void CppMidHookWrapper(safetyhook::Context& ctx)
{
    if (!MFunctionHook::s_currentInstance || !MFunctionHook::s_currentInstance->m_userCallback)
    {
        return;
    }

    __try
    {
        auto callback = (void (*)(void*))MFunctionHook::s_currentInstance->m_userCallback;
        callback(&ctx);
    }
    __except (EXCEPTION_EXECUTE_HANDLER)
    {
        printf("[MFunctionHook::CppWrapper] EXCEPTION during callback! Code: 0x%X\n", GetExceptionCode());
    }
}

void MFunctionHook::SetHookFunction(void* addr, void* callback)
{
    if (!addr || !callback)
    {
        return;
    }

    m_userCallback = callback;
    s_currentInstance = this;
    m_oHook = safetyhook::create_mid(addr, CppMidHookWrapper, safetyhook::MidHook::StartDisabled);
}

void MFunctionHook::Enable()
{
    if (m_oHook.enabled())
    {
        return;
    }

    auto result = m_oHook.enable();
}

void MFunctionHook::Disable()
{
    if (!m_oHook.enabled())
        return;

    m_oHook.disable();
}

bool MFunctionHook::IsEnabled()
{
    return m_oHook.enabled();
}