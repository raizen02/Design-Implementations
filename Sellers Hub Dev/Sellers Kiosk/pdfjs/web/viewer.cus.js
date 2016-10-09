// JScript File

function base64toBlob(base64Data, contentType) {
    contentType = contentType || '';
    var sliceSize = 1024;
    var byteCharacters = atob(base64Data);
    var bytesLength = byteCharacters.length;
    var slicesCount = Math.ceil(bytesLength / sliceSize);
    var byteArrays = new Array(slicesCount);

    for (var sliceIndex = 0; sliceIndex < slicesCount; ++sliceIndex) {
        var begin = sliceIndex * sliceSize;
        var end = Math.min(begin + sliceSize, bytesLength);

        var bytes = new Array(end - begin);
        for (var offset = begin, i = 0 ; offset < end; ++i, ++offset) {
            bytes[i] = byteCharacters[offset].charCodeAt(0);
        }
        byteArrays[sliceIndex] = new Uint8Array(bytes);
    }
    return new Blob(byteArrays, { type: contentType });
}

function OpenPDF(base64Data, fileName) {
    var blob = base64toBlob(base64Data, "application/pdf");

    var fr = new FileReader();
    fr.onload = function() {
        var arraybuffer = this.result;
        var uint8array = new Uint8Array(arraybuffer);
        PDFJS.workerSrc = 'pdfjs/build/pdf.worker.js';
        PDFView.url = fileName == '' ? 'document.pdf' : fileName ;
        PDFView.open(uint8array, 0);
    };

    fr.readAsArrayBuffer(blob);
}

function OpenPDFErr(msg) {
     PDFJS.workerSrc = 'pdfjs/build/pdf.worker.js';
     PDFView.error(msg, null);
}