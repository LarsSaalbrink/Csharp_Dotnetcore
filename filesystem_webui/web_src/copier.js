const ncp = require('ncp').ncp;
const glob = require('glob');

// Define the source and destination directories
const sourceDir = './src';
const destDir = '../wwwroot';

// Use glob to match all files in the source directory except TypeScript files
glob(`${sourceDir}/**/!(*.ts)`, (err, files) => {
    if (err) throw err;

    // Copy each matched file to the destination directory
    files.forEach(file => {
        const destFile = file.replace(sourceDir, destDir);
        ncp(file, destFile, err => {
            if (err) throw err;
        });
    });
});