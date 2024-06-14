mergeInto(LibraryManager.library, {
    StringReturnValueFunction: function()
    {
        var playerOneName = parent.document.getElementById('playerOneName').value;
        var playerTwoName = parent.document.getElementById('playerTwoName').value;
        var returnStr = JSON.stringify({ playerOneName: playerOneName, playerTwoName: playerTwoName });
        var bufferSize = lengthBytesUTF8(returnStr) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(returnStr, buffer, bufferSize);
        return buffer;
    }
});