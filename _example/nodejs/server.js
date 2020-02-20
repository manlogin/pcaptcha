const express = require("express");
const app = express();
const http = require("http").createServer(app);
const axios = require("axios");
const https = require("https");

app.set("view engine", "ejs");
app.use(express.static(__dirname + '/css'));

app.get("/", (req, res) => {
    res.render("index")
});

app.get("/recaptcha", (req, res) => {

    let pcaptcha  = req.query.pcaptcha; // مقداری است که از طریق فرم ارسال شده است
    let uid       = "0x7537"; // کد یکتای کپچای شما در سایت من لاکین
    let secretKey = "18d9d62dd1537ce2c4810d4286650f7d880a2c59b8868c6dd6a52fcd6036e84f"; // کلید خصوصی
    let url       = "https://manlogin.com/captcha/cheack/v1/" + uid + "/" + secretKey + "/" + pcaptcha;
    const agent = new https.Agent({
        rejectUnauthorized: false
    });
    axios.get(url, { httpsAgent: agent })
        .then(function (response) {

            if (response.status == 200) {
                res.send("<h1>کپچا مورد تایید است</h1>")
            } else {
                res.send("<h1>دوباره امتحان کنید</h1>")
            }

        })
        .catch(function (error) {
            res.send("<h1>مشکلی رخ داده است</h1>");
        })

});


http.listen(4000, () => {
    console.log("*** Listen On Port 4000 ***")
});