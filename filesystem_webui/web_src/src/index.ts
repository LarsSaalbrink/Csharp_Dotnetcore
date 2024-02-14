interface File {
    Name: string;
    Path: string;
    Size: number;
    ParentFolder: string | null;
}
// Parse the JSON response from /read_fs and display the files in the files_list div
function parse_filesys_json(json: string): void {
    const files = JSON.parse(json);
    const filesList = document.getElementById('files_list');

    if (filesList === null) {
        console.error('files_list element not found');
    } else {
        filesList.innerHTML = '';
        files.forEach((file: File) => {
            const fileDiv = `
            <div class="file">
                <div class="file_name">${file.Name}</div>
                <div class="file_size">${file.Size}</div>
            </div>
        `;

            filesList.innerHTML += fileDiv;
        });
    }
}

// HTTP GET request to /read_fs, then pass the response to parse_filesys_json
function get_files(): void {
    fetch('/read_fs').then(response => response.text()).then(parse_filesys_json);
}
