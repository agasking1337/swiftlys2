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

#ifndef src_memory_hooks_mfunction_h
#define src_memory_hooks_mfunction_h

#include <api/memory/hooks/mfunction.h>
#include <safetyhook/safetyhook.hpp>
#include <shared_mutex>
#include <unordered_map>

class MFunctionHook : public IMFunctionHook
{
public:
    MFunctionHook() = default;
    ~MFunctionHook()
    {
        if (m_hookAddress)
        {
            std::unique_lock<std::shared_mutex> lock(s_instancesMutex);
            s_instances.erase(m_hookAddress);
        }
    }

    virtual void SetHookFunction(void* addr, void* callback) override;

    virtual void Enable() override;
    virtual void Disable() override;
    virtual bool IsEnabled() override;

    static std::unordered_map<void*, MFunctionHook*> s_instances;
    static std::shared_mutex s_instancesMutex;

    void* m_hookAddress = nullptr;
    void* m_userCallback = nullptr;

private:
    SafetyHookMid m_oHook;
};

#endif