var gulp = require("gulp");
var paths = {
    webroot: "./wwwroot/"
};

gulp.task('copy-js', function () {
    gulp.src('./node_modules/jquery/dist/jquery.js')
        .pipe(gulp.dest(paths.webroot + '/assets/npm'));
});
gulp.task('build', function () {

});  