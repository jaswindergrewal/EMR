
//function to validate Photo uploading 
function UploadImageValidate(){        
    var fileName = $("#MainContent_ImageUpload").val();           
    var fileExtension = fileName.substr(fileName.indexOf(".") + 1);           
    if (fileName == "" )
    {
        alert('Please specify the Image to upload');
        return false;
    }
    else if (fileExtension == "gif" || fileExtension == "jpg" || fileExtension == "png") {
        alert('Image uploaded successfully');
        return true;
    }
    else {
        alert("The type of extension should be gif/jpg/png");
        return false;
    }
}
