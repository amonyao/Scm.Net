<template>
    <div @click="inputFocus" class="my-reply">
        <el-avatar class="header-img" :size="40" :src="myHeader"></el-avatar>
        <div class="reply-info">
            <div tabindex="0" contenteditable="true" id="replyInput" spellcheck="false" placeholder="输入评论..."
                class="reply-input" @focus="showReplyBtn" @input="onDivInput($event)">
            </div>
        </div>
        <div disabled>
            <el-button plain icon="el-icon-picture">表情</el-button>
            <el-button plain icon="el-icon-picture">图片</el-button>
        </div>
        <div class="reply-btn-box" v-show="btnShow">
            <el-button class="reply-btn" @click="sendComment" type="primary">发表评论</el-button>
        </div>
    </div>
</template>
<script>
export default {
    emits: ['complete'],
    props: {
        placeholder: {}
    },
    data() {
        return {
            replyComment: '',
            btnShow: false,
            myHeader: 'https://ae01.alicdn.com/kf/Hd60a3f7c06fd47ae85624badd32ce54dv.jpg',
        };
    },
    methods: {
        inputFocus() {
            this.replyInput = document.getElementById('replyInput');
            this.replyInput.style.padding = "8px 8px"
            this.replyInput.style.border = "2px solid blue"
            this.replyInput.focus()
        },
        myFocus() {
            this.btnShow = true;
        },
        myBlur() {
            this.btnShow = !!this.comment;
        },
        showReplyBtn() {
            this.btnShow = true
        },
        hideReplyBtn() {
            this.btnShow = false
            this.replyInput.style.padding = "10px"
            this.replyInput.style.border = "none"
        },
        onDivInput: function (e) {
            this.replyComment = e.target.innerHTML;
        },
        sendComment() {
            if (!this.replyComment) {
                this.$message({
                    showClose: true,
                    type: 'warning',
                    message: '评论不能为空'
                })
            } else {
                let a = {}
                let input = document.getElementById('replyInput')
                let timeNow = new Date().getTime();
                let time = this.dateStr(timeNow);
                a.name = this.myName
                a.comment = this.replyComment
                a.headImg = this.myHeader
                a.time = time
                a.commentNum = 0
                a.like = 0
                this.comments.push(a)
                this.replyComment = ''
                input.innerHTML = '';

            }
        },
        sendCommentReply(i) {
            if (!this.replyComment) {
                this.$message({
                    showClose: true,
                    type: 'warning',
                    message: '评论不能为空'
                })
            } else {
                let a = {}
                let timeNow = new Date().getTime();
                let time = this.dateStr(timeNow);
                a.from = this.myName
                a.to = this.to
                a.fromHeadImg = this.myHeader
                a.comment = this.replyComment
                a.time = time
                a.commentNum = 0
                a.like = 0
                this.comments[i].reply.push(a)
                this.replyComment = ''
                document.getElementsByClassName("reply-comment-input")[i].innerHTML = ""
            }
        },
    }
}
</script>

<style>
.my-reply {
    padding: 10px;
    background-color: #fafbfc;
}

.my-reply .header-img {
    display: inline-block;
    vertical-align: top;
}

.my-reply .reply-info {
    display: inline-block;
    margin-left: 5px;
    width: 90%;
}

@media screen and (max-width: 1200px) {
    .my-reply .reply-info {
        width: 80%;
    }
}

.my-reply .reply-info .reply-input {
    min-height: 20px;
    line-height: 22px;
    padding: 10px 10px;
    color: #ccc;
    background-color: #fff;
    border-radius: 5px;
}

.my-reply .reply-info .reply-input:empty:before {
    content: attr(placeholder);
}

.my-reply .reply-info .reply-input:focus:before {
    content: none;
}

.my-reply .reply-info .reply-input:focus {
    padding: 8px 8px;
    border: 2px solid #00f;
    box-shadow: none;
    outline: none;
}

.my-reply .reply-btn-box {
    height: 25px;
    margin: 10px 0;
}

.my-reply .reply-btn-box .reply-btn {
    position: relative;
    float: right;
    margin-right: 15px;
}
</style>