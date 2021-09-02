using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace IcqBotNetCore.BusinessObjects.Commands
{
    [DataContract]
    public enum ParamTypeResponeEnum
    {
        caption,
        chatId,
        fileId,
        format,
        forwardChatId,
        forwardMsgId,
        inlineKeyboardMarkup,
        lastEventId,
        msgId,
        parseMode,
        pollTime,
        replyMsgId,
        showAlert,
        text,
        token,
        url
    }
}
