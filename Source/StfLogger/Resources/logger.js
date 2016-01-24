var lastAnchor = "";

/* Hide buttons if no messages are present - good idea, not implemented fully yet */
var PassCount = 1;
var FailCount = 1;
var ErrorCount = 1;
var WarningCount = 1;
var InfoCount = 1;
var DebugCount = 1;
var TraceCount = 1;
var InternalCount = 1;

var MessegeType = "inline";

function toggle_messege(messageId) {
    document.getElementById(messageId).style.display = MessegeType;

    if (MessegeType === "inline") {
        MessegeType = "none";
    } else {
        MessegeType = "inline";
    }
}


function getURLParameter(name) {
    return decodeURIComponent((new RegExp("[?|&]" + name + "=" + "([^&;]+?)(&|#|;|$)").exec(location.search) || [, ""])[1].replace(/\+/g, "%20")) || null;
}

function toggleKeyValues() {
    if (document.getElementById("toggleKeyValues").value === "Hide") {
        css("ul#logfileinfo", "display", "none");
        toggleButton("toggleKeyValues", "Show", "Show KeyValues");
    } else if (document.getElementById("toggleKeyValues").value === "Show") {
        var headerHeight = getHeaderHeight();
        document.getElementById("logfileinfo").style.top = headerHeight + "px";
        css("ul#logfileinfo", "display", "Block");
        setKeyValuesCenter();
        toggleButton("toggleKeyValues", "Hide", "Hide KeyValues");
    }
}

// as per http://stackoverflow.com/questions/7410949/javascript-document-getelementsbyclassname-compatibility-with-ie
if (!document.getElementsByClassName) {
    document.getElementsByClassName = function (className) {
        return this.querySelectorAll("." + className);
    };
    Element.prototype.getElementsByClassName = document.getElementsByClassName;
}

function css(selector, property, value) {
    for (var i = 0; i < document.styleSheets.length; i++) { //Loop through all styles
        try {
            document.styleSheets[i].insertRule(selector + " {" + property + ":" + value + "}", document.styleSheets[i].cssRules.length);
        } catch (err) {
            try {
                document.styleSheets[i].addRule(selector, property + ":" + value);
            } catch (err) { }
        } //IE 
    }
}

function toggleButton(buttonId, value, text) {
    document.getElementById(buttonId).value = value;
    document.getElementById(buttonId).innerHTML = text;
}

function toggleLogElements(divElement, buttonId, logElement) {
    if (document.getElementById(buttonId).value === "Hide") {
        css(divElement, "display", "none");
        toggleButton(buttonId, "Show", "Show " + logElement);
    } else if (document.getElementById(buttonId).value === "Show") {
        css(divElement, "display", "Block");
        toggleButton(buttonId, "Hide", "Hide " + logElement);
    }

    goToLastAnchor();
}

function loadKeyValueList() {
    var logFileInfoList = document.getElementById("logfileinfo");
    var keyValueList = document.getElementsByClassName("keyvalue");

    for (var i = 0; i < keyValueList.length; i++) {
        var key = keyValueList[i].children[0].innerHTML;
        var dupKeyFound = false;

        // search to see if a similar key has been set after this one
        for (var j = i+1; j < keyValueList.length; j++) {
            var thisKey = keyValueList[j].children[0].innerHTML;
            if (key === thisKey) {
                dupKeyFound = true;
                break;
            }
        }

        if (dupKeyFound) {
            continue;
        }

        var logFileInfoListItem = document.createElement("li");
        var keyItem = document.createElement("b");
        var valueItem = document.createElement("em");
        var value = keyValueList[i].children[1].innerHTML;
        var hoverMessage = keyValueList[i].children[2].innerHTML;
        var keyItemText = document.createTextNode(key + ": ");
        var valueItemText = document.createTextNode(value);

        if ((value.toLowerCase().indexOf("http:") == 0)
         || (value.toLowerCase().indexOf("td:") == 0)
         || (value.toLowerCase().indexOf("file:") == 0)) {
            var link = document.createElement("a");
            link.setAttribute("href", value);
            link.setAttribute("title", hoverMessage);
            link.textContent = value;
            valueItemText = link;
        }

        keyItem.appendChild(keyItemText);
        valueItem.appendChild(valueItemText);
        logFileInfoListItem.appendChild(keyItem);
        logFileInfoListItem.appendChild(valueItem);
        logFileInfoList.appendChild(logFileInfoListItem);
    }

    initButtons();
    updateHeaderWithStatus();
}

function displayAllElements(blockOrNone) {
    //css('.pass', 'display', blockOrNone);  
    //css('.fail', 'display', blockOrNone);   
    css(".error", "display", blockOrNone);
    css(".warning", "display", blockOrNone);
    css(".info", "display", blockOrNone);
    css(".debug", "display", blockOrNone);
    css(".trace", "display", blockOrNone);
    css(".internal", "display", blockOrNone);
}

function toggleAll() {
    var buttonId = "toggleAllBtn";
    if (document.getElementById(buttonId).value === "Hide") {
        displayAllElements("none");
        toggleButton(buttonId, "Show", "Show All");
        //toggleButton("passBtn", "Show", "Show Pass") 
        //toggleButton("failBtn", "Show", "Show Fail") 
        toggleButton("errorBtn", "Show", "Show Error");
        toggleButton("warningBtn", "Show", "Show Warning");
        toggleButton("infoBtn", "Show", "Show Info");
        toggleButton("debugBtn", "Show", "Show Debug");
        toggleButton("traceBtn", "Show", "Show Trace");
        toggleButton("internalBtn", "Show", "Show Internal");
    } else if (document.getElementById(buttonId).value === "Show") {
        displayAllElements("block");
        toggleButton(buttonId, "Hide", "Hide All");
        toggleButton("passBtn", "Hide", "Hide Pass");
        toggleButton("failBtn", "Hide", "Hide Fail");
        toggleButton("errorBtn", "Hide", "Hide Error");
        toggleButton("warningBtn", "Hide", "Hide Warning");
        toggleButton("infoBtn", "Hide", "Hide Info");
        toggleButton("debugBtn", "Hide", "Hide Debug");
        toggleButton("traceBtn", "Hide", "Hide Trace");
        toggleButton("internalBtn", "Hide", "Hide Internal");
    }
}

function toggleIndent() {
    var buttonId = "toggleIndent";
    if (document.getElementById(buttonId).value === "Hide") {
        toggleButton(buttonId, "Show", "Show Depth");
        css("div.pad", "display", "none");
    } else if (document.getElementById(buttonId).value === "Show") {
        toggleButton(buttonId, "Hide", "Hide Depth");
        css("div.pad", "display", "Block");
    }
}

function getHeaderHeight() {
    var headerHeight = document.getElementById("header").clientHeight;
    return headerHeight;
}


function pushDownLogElements() {
    var headerHeight = getHeaderHeight();
    headerHeight = headerHeight + 5;
    document.getElementById("logfileelements").style.marginTop = headerHeight + "px";
}

function setKeyValuesCenter() {
    var widthOfHeader = document.getElementById("header").clientWidth;
    var widthOfKeyValues = document.getElementById("logfileinfo").clientWidth;
    var marginLeftKeyValues = (widthOfHeader - widthOfKeyValues) / 2;

    document.getElementById("logfileinfo").style.marginLeft = marginLeftKeyValues + "px";
}

function showImage(image) {
    var url = image.getAttribute("src");
    window.open(url, "Image", "resizable=1");
}

function initLogFile() {
    loadKeyValueList();
    pushDownLogElements();
}

function addLastAnchor() {
    window.location.hash = lastAnchor;
}

function goToLastAnchor() {
    if (lastAnchor.length > 0) {
        document.getElementById(lastAnchor).scrollIntoView();
        var scrollPositionArray = getScrollingPosition();
        var newScrollPosition = scrollPositionArray[1] + getHeaderHeight();
        window.scrollTo(0, newScrollPosition);
    }
}

function sa(anchorName) {
    lastAnchor = (anchorName);
}

function getScrollingPosition() {
    var position = [0, 0];

    if (typeof window.pageYOffset != "undefined") {
        position = [window.pageXOffset, window.pageYOffset];
    } else if (typeof document.documentElement.scrollTop != "undefined" && document.documentElement.scrollTop > 0) {
        position = [document.documentElement.scrollLeft, document.documentElement.scrollTop];
    } else if (typeof document.body.scrollTop != "undefined") {
        position = [document.body.scrollLeft, document.body.scrollTop];
    }

    return position;
}

function hideButtonByDisplayCount(buttonSelector, displayCount) {
    if (displayCount === 0) {
        document.getElementById(buttonSelector).disabled = true;
    }
}

function updateHeaderWithStatus() {
    var statusText = document.getElementById("runstatus").innerHTML;
    var currentGenereatedBy = document.getElementById("generatedbyOvid").innerHTML;
    document.getElementById("generatedbyOvid").innerHTML = statusText + " - " + currentGenereatedBy;
}

function initButtonCount(className) {
    return document.getElementsByClassName("line " + className).length;
}

function initButtons() {
    if (getURLParameter("hidebuttons") === "1") {
        css("div#buttons", "display", "none");
    } else {
        css("siv#buttons", "display", "block");
    }

    PassCount = initButtonCount("pass");
    FailCount = initButtonCount("fail");
    ErrorCount = initButtonCount("error");
    WarningCount = initButtonCount("warning");
    InfoCount = initButtonCount("info");
    DebugCount = initButtonCount("debug");
    TraceCount = initButtonCount("trace");
    InternalCount = initButtonCount("internal");
    hideButtonByDisplayCount("passBtn", PassCount);
    hideButtonByDisplayCount("failBtn", FailCount);
    hideButtonByDisplayCount("errorBtn", ErrorCount);
    hideButtonByDisplayCount("warningBtn", WarningCount);
    hideButtonByDisplayCount("infoBtn", InfoCount);
    hideButtonByDisplayCount("debugBtn", DebugCount);
    hideButtonByDisplayCount("traceBtn", TraceCount);
    hideButtonByDisplayCount("internalBtn", InternalCount);
}
