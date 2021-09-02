using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IcqBotNetCore.BusinessObjects.Commands
{
    [DataContract]
    public enum ApiCommandEnum
    {
        AnswerCallbackQuery,
        BlockUser,
        DeleteMember,
        DeleteMessage,
        EditText,
        GetAdmins,
        GetBlockedUsers,
        GetEvents,
        GetFileInfo,
        GetInfo,
        GetMembers,
        PinMessage,
        ResolvePending,
        SelfGet,
        SendActions,
        SendFile,
        SendText,
        SendVoice,
        SetAbout,
        SetAvatar,
        SetRules,
        SetTitle,
        UnblockUser,
        UnpinMessage
    }
}
