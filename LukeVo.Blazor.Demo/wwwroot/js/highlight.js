window.highlightAll = function () {
    document.querySelectorAll("pre[data-highlight] > code").forEach(el => {
        hljs.highlightBlock(el);
    });
}