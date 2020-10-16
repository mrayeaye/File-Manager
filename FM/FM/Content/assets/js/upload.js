const fileTempl = document.getElementById("file-template"),
    imageTempl = document.getElementById("image-template"),
    empty = document.getElementById("empty");

// use to store pre selected files
let FILES = {};
let deleteflag = 0;



// check if file is of type image and prepend the initialied
// template to the target element
function addFile(target, file) {
    const isImage = file.type.match("image.*"),
        objectURL = URL.createObjectURL(file);
    
    const clone = isImage ?
        imageTempl.content.cloneNode(true) :
        fileTempl.content.cloneNode(true);

    clone.querySelector("h1").textContent = file.name;
    clone.querySelector("li").id = objectURL;
    clone.querySelector(".delete").dataset.target = objectURL;
    clone.querySelector(".size").textContent =
        file.size > 1024 ?
        file.size > 1048576 ?
        Math.round(file.size / 1048576) + "mb" :
        Math.round(file.size / 1024) + "kb" :
        file.size + "b";

    isImage &&
        Object.assign(clone.querySelector("img"), {
            src: objectURL,
            alt: file.name
        });

    empty.classList.add("hidden");
    target.prepend(clone);

    FILES[file.name] = file;
    console.log(file.name);
   
  
}

const gallery = document.getElementById("gallery"),
    overlay = document.getElementById("overlay");

// click the hidden input of type file if the visible button is clicked
// and capture the selected files
const hidden = document.getElementById("hidden-input");
document.getElementById("button").onclick = () => {
    hidden.click(); deleteflag = 0;
}
hidden.onchange = (e) => {
    for (const file of e.target.files) {
        addFile(gallery, file);
    }
    
};

// use to check if a file is being dragged
const hasFiles = ({ dataTransfer: { types = [] } }) =>
    types.indexOf("Files") > -1;

// use to drag dragenter and dragleave events.
// this is to know if the outermost parent is dragged over
// without issues due to drag events on its children
let counter = 0;

// reset counter and append file to gallery when file is dropped
function dropHandler(ev) {
    ev.preventDefault();
    for (const file of ev.dataTransfer.files) {
        addFile(gallery, file);
        overlay.classList.remove("draggedover");
        counter = 0;
    }
}

// only react to actual files being dragged
function dragEnterHandler(e) {
    e.preventDefault();
    if (!hasFiles(e)) {
        return;
    }
    ++counter && overlay.classList.add("draggedover");
}

function dragLeaveHandler(e) {
    1 > --counter && overlay.classList.remove("draggedover");
}

function dragOverHandler(e) {
    if (hasFiles(e)) {
        e.preventDefault();
    }
}

// event delegation to caputre delete events
// fron the waste buckets in the file preview cards
gallery.onclick = ({ target }) => {
    if (target.classList.contains("delete")) {
       
        const ou = target.dataset.target;
        document.getElementById(ou).remove(ou);
        gallery.children.length === 1 && empty.classList.remove("hidden");
        delete FILES[ou];
        console.log(ou);
    }
};

// print all selected files
document.getElementById("submit").onclick = () => {
    //alert(`Submitted Files:\n${JSON.stringify(FILES)}`);

    // Checking whether FormData is available in browser  
    if (window.FormData !== undefined) {

        var fileUpload = $("#hidden-input").get(0);
        var files = fileUpload.files;
       
        // Create FormData object  
        
        try {
            // Looping over all files and add it to FormData object  
            if (deleteflag == 0) {
                for (var i = 0; i < files.length; i++) {
                    var fileData = new FormData();
                    // objectURL = URL.createObjectURL(files[i]);
                    // fileData.append("ss", FILES[objectURL]);
                    console.log(FILES[files[i].name]);
                    fileData.append("blob", FILES[files[i].name], files[i].name);


                    var xhttp = new XMLHttpRequest();

                    xhttp.open("POST", "/Home/Landing", false);

                    xhttp.onreadystatechange = function () {

                        if (this.readyState == 4 && this.status == 200) {
                            Myresponse = JSON.parse(this.responseText)
                            console.log(Myresponse);

                        }
                    };

                    xhttp.send(fileData);

                }
            }
        }
        catch (ex) {
            console.log(ex.responseText);
        }
        // Adding one more key to FormData object  
        //fileData.append('username', 'Manas');       
    //    $.ajax({
    //        url: '/Home/Landing',
    //        type: "POST",
    //        contentType: false, // Not to set any content header  
    //        processData: false, // Not to process data  
    //        data: fileData,
    //        success: function (result) {
    //            alert(result);
    //        },
    //        error: function (err) {
    //            alert(err.statusText);             
    //        }
    //    });
    //} else {
    //    alert("FormData is not supported.");
    }  
    console.log(FILES);
};
function loadDoc() {

    

}
// clear entire selection
document.getElementById("cancel").onclick = () => {
    while (gallery.children.length > 0) {
        gallery.lastChild.remove();
    }
    FILES = {};
    empty.classList.remove("hidden");
    gallery.append(empty);
    deleteflag = 1;
};