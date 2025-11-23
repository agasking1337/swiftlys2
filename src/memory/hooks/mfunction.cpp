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

std::unordered_map<void*, MFunctionHook*> MFunctionHook::s_instances;
std::shared_mutex MFunctionHook::s_instancesMutex;

static void MidHookWrapper(safetyhook::Context& ctx)
{
    std::shared_lock<std::shared_mutex> lock(MFunctionHook::s_instancesMutex);

    for (const auto& pair : MFunctionHook::s_instances)
    {
        MFunctionHook* instance = pair.second;
        if (instance && instance->IsEnabled() && instance->m_userCallback)
        {
            // Release lock before calling into managed code to avoid deadlocks
            lock.unlock();

            auto callback = (void (*)(void*))instance->m_userCallback;
            callback(&ctx);

            return;
        }
    }
}

void MFunctionHook::SetHookFunction(void* addr, void* callback)
{
    if (!addr || !callback)
    {
        return;
    }

    m_hookAddress = addr;
    m_userCallback = callback;

    // Register this instance in global map
    {
        std::unique_lock<std::shared_mutex> lock(s_instancesMutex);
        s_instances[addr] = this;
    }

    m_oHook = safetyhook::create_mid(addr, MidHookWrapper, safetyhook::MidHook::StartDisabled);
}

void MFunctionHook::Enable()
{
    if (m_oHook.enabled())
    {
        return;
    }
    (void)m_oHook.enable();
}

void MFunctionHook::Disable()
{
    if (!m_oHook.enabled())
    {
        return;
    }
    (void)m_oHook.disable();
}

bool MFunctionHook::IsEnabled()
{
    return m_oHook.enabled();
}