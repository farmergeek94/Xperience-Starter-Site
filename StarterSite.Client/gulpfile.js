/// <binding ProjectOpened='Watch' />
//list dependences
import gulp from 'gulp';
const { series, parallel, src, dest, task, watch } = gulp;
import rename from 'gulp-rename';
import * as dartSass from 'sass';
import gulpSass from 'gulp-sass';
const sass = gulpSass(dartSass);
import prefix from 'gulp-autoprefixer';
import terser from 'gulp-terser';
import gzip from 'gulp-gzip';
import concat from 'gulp-concat';
import webpack from 'webpack';
import cleanCSS from 'gulp-clean-css';

function bootstrapSass(cb) {
    src("src/scss/StarterSite.scss")
        .pipe(sass())
        .pipe(prefix())
        .pipe(rename({ extname: ".css" }))
        .pipe(dest("src/css"));
    cb();
}


//const webpackConfigReactBlog from './FrontEndDev/react/components/Blog/webpack.config');
//function blogReactApp() {
//    return new Promise((resolve, reject) => {
//        webpack(webpackConfigReactBlog, (err, stats) => {
//            if (err) {
//                return reject(err)
//            }
//            if (stats.hasErrors()) {
//                return reject(new Error(stats.compilation.errors.join('\n')))
//            }
//            resolve()
//        })
//    }).then(() => {
//        return src("wwwroot/js/scripts/components/blog.js", { base: "wwwroot/" })
//            .pipe(gzip())
//            .pipe(rename("blog.js.gz"))
//            .pipe(dest("wwwroot/js/scripts/components"));
//    });
//}


/* Bunding / minifying CSS / Javascript */
function headerCss(cb) {
    src([
        "src/css/StarterSite.css",
    ], { base: "wwwroot/" })
        .pipe(concat("HeaderStyles.css"))
        .pipe(cleanCSS({
            level: 2
        }))
        .pipe(dest('wwwroot/css/bundles'))
        .pipe(rename("HeaderStyles.min.css"))
        .pipe(dest('wwwroot/css/bundles'))
        .pipe(gzip())
        .pipe(rename("HeaderStyles.min.css.gz"))
        .pipe(dest("wwwroot/css/bundles"));
    cb();
}

function headerJs(cb) {
    src([
        "wwwroot/js/scripts/jquery-3.6.1.min.js"
    ], { base: "wwwroot/" })
        .pipe(concat("HeaderJS.js"))
        .pipe(dest('wwwroot/js/bundles'))
        .pipe(terser())
        .pipe(rename("HeaderJS.min.js"))
        .pipe(dest('wwwroot/js/bundles'))
        .pipe(gzip())
        .pipe(rename("HeaderJS.min.js.gz"))
        .pipe(dest('wwwroot/js/bundles'));
    cb();
}

function validateJs(cb) {
    src([
        "wwwroot/js/jquery.validate/jquery.validate.min.js",
        "wwwroot/js/jquery.validate/jquery.validate.unobtrusive.min.js",
        "wwwroot/js/jquery.validate/jquery.validate-vsdoc.js"
    ], { base: "wwwroot/" })
        .pipe(concat("jqueryval.js"))
        .pipe(dest('wwwroot/js/bundles'))
        .pipe(terser())
        .pipe(rename("jqueryval.min.js"))
        .pipe(dest('wwwroot/js/bundles'))
        .pipe(gzip())
        .pipe(rename("jqueryval.min.js.gz"))
        .pipe(dest('wwwroot/js/bundles'));
    cb();
}

function footerJs(cb) {
    // Can optionally include your react apps here as well
    src([
        "node_modules/bootstrap/dist/js/bootstrap.js",
    ], { base: "wwwroot/" })
        .pipe(concat("FooterJS.js"))
        .pipe(dest('wwwroot/js/bundles'))
        .pipe(terser())
        .pipe(rename("FooterJS.min.js"))
        .pipe(dest('wwwroot/js/bundles'))
        .pipe(gzip())
        .pipe(rename("FooterJS.min.js.gz"))
        .pipe(dest('wwwroot/js/bundles'));
    cb();
}

// packages.json's development task (npm run development) is set to execute this watch on visual studio open.
const development = (cb) => {
    //watch(["wwwroot/css/custom.css"], series(headerCss));
    watch(["src/scss/*.scss"], series(bootstrapSass, headerCss));
    //watch(["FrontEndDev/react/components/blog/*.{ts,tsx,jsx}"], series(blogReactApp));
    //watch(["wwwroot/js/scripts/custom/*.{js}"], series(footerJs));
    cb();
}

const build = parallel(
    series(bootstrapSass, headerCss),
    //series(headerJs, validateJs),
    series(footerJs),
    //series(blogReactApp)
);

export {
    development,
    build
} 