var gulp = require('gulp');
var ts = require('gulp-typescript');
var watch = require('gulp-watch');
var serve = require('gulp-serve');

var tsProjectOptions = {
  target: 'ES5',
  // declarationFiles: false,
};

gulp.task('serve', serve('.'));

gulp.task('dev', gulp.series(scripts, setWatchers));

function setWatchers() {
  gulp.watch(['app/**/*.ts'], gulp.series(scripts));
}

var tsProject = ts.createProject(tsProjectOptions);
function scripts() {
  var files = gulp.src(['app/**/*.ts'], { base: "." })
    .pipe(ts(tsProject));

  return files.pipe(gulp.dest('.'));
}
