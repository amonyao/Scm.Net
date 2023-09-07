<template>
    <el-card>
        <div class="sc-comment">
            <div class="post">
                <div class="head">
                    <h3>发表评论</h3>
                </div>
                <div class="comment">
                    <myComment @save="saveComment"></myComment>
                </div>
            </div>
            <div class="list">
                <div class="head">
                    <el-row>
                        <el-col :span="12">
                            <h3>全部评论 {{ 14 }}</h3>
                        </el-col>
                        <el-col :span="12" style="text-align: right;">
                            <el-radio-group v-model="sort">
                                <el-radio-button label="0">默认</el-radio-button>
                                <el-radio-button label="1">最新</el-radio-button>
                                <el-radio-button label="2">最热</el-radio-button>
                            </el-radio-group>
                        </el-col>
                    </el-row>
                </div>
                <div class="sc-comment-list">
                    <div class="sc-comment-item" v-for="item in comments" :key="item.id">
                        <div class="left" width="60px">
                            <el-avatar class="header-img" :size="40" :src="item.headImg"></el-avatar>
                        </div>
                        <div class="body">
                            <el-row>
                                <el-col :span="20" style="text-align: left;">
                                    {{ item.name }}
                                </el-col>
                                <el-col :span="4" style="text-align: right;">
                                    <el-icon>
                                        <Star />
                                    </el-icon>
                                    {{ item.like }}
                                </el-col>
                            </el-row>
                            <el-row>
                                <el-col>
                                    <div class="ref">
                                        @用户名称:引用内容
                                    </div>
                                    <div class="comment">
                                        {{ item.comment }}
                                    </div>
                                </el-col>
                            </el-row>
                            <el-row>
                                <el-col>
                                    <el-button size="small" @click="showReplyInput(item)">
                                        {{ item.commentNum }} 回复 &gt;
                                    </el-button>
                                    <span class="time">
                                        <label style="line-height: 24px;">{{ item.time }}</label>
                                    </span>
                                </el-col>
                            </el-row>
                        </div>
                    </div>
                </div>
            </div>
            <div class="reply" v-if="toId != '0'">
                <myComment ref="reply" @save="saveComment"></myComment>
            </div>
        </div>
    </el-card>
</template>

<script>
import { defineAsyncComponent } from "vue";
import * as ElementPlusIconsVue from '@element-plus/icons-vue'
const clickoutside = {
    // 初始化指令
    bind(el, binding) {
        function documentHandler(e) {
            // 这里判断点击的元素是否是本身，是本身，则返回
            if (el.contains(e.target)) {
                return false;
            }
            // 判断指令中是否绑定了函数
            if (binding.expression) {
                // 如果绑定了函数 则调用那个函数，此处binding.value就是handleClose方法
                binding.value(e);
            }
        }
        // 给当前元素绑定个私有变量，方便在unbind中可以解除事件监听
        el.vueClickOutside = documentHandler;
        document.addEventListener('click', documentHandler);
    },
    update() { },
    unbind(el) {
        // 解除事件监听
        document.removeEventListener('click', el.vueClickOutside);
        delete el.vueClickOutside;
    },
};
export default {
    components: {
    myComment: defineAsyncComponent(() => import("./comment.vue")),
    ElementPlusIconsVue: ElementPlusIconsVue,
},
    name: 'ArticleComment',
    data() {
        return {
            sort: '0',
            to: '',
            toId: '0',
            comments: ''
        }
    },
    directives: { clickoutside },
    mounted() {
        this.init();
    },
    methods: {
        init() {
            this.comments = [
                {
                    name: 'Lana Del Rey',
                    id: 19870621,
                    headImg: 'https://ae01.alicdn.com/kf/Hd60a3f7c06fd47ae85624badd32ce54dv.jpg',
                    comment: '我发布一张新专辑Norman Fucking Rockwell,大家快来听啊',
                    time: '2019年9月16日 18:43',
                    commentNum: 2,
                    like: 15,
                    inputShow: false,
                    reply: [
                        {
                            from: 'Taylor Swift',
                            fromId: 19891221,
                            fromHeadImg: 'https://ae01.alicdn.com/kf/H94c78935ffa64e7e977544d19ecebf06L.jpg',
                            to: 'Lana Del Rey',
                            toId: 19870621,
                            comment: '我很喜欢你的新专辑！！',
                            time: '2019年9月16日 18:43',
                            commentNum: 1,
                            like: 15,
                            inputShow: false
                        },
                        {
                            from: 'Ariana Grande',
                            fromId: 1123,
                            fromHeadImg: 'https://ae01.alicdn.com/kf/Hf6c0b4a7428b4edf866a9fbab75568e6U.jpg',
                            to: 'Lana Del Rey',
                            toId: 19870621,
                            comment: '别忘记宣传我们的合作单曲啊',
                            time: '2019年9月16日 18:43',
                            commentNum: 0,
                            like: 5,
                            inputShow: false

                        }
                    ]
                },
                {
                    name: 'Taylor Swift',
                    id: 19891221,
                    headImg: 'https://ae01.alicdn.com/kf/H94c78935ffa64e7e977544d19ecebf06L.jpg',
                    comment: '我发行了我的新专辑Lover',
                    time: '2019年9月16日 18:43',
                    commentNum: 1,
                    like: 5,
                    inputShow: false,
                    reply: [
                        {
                            from: 'Lana Del Rey',
                            fromId: 19870621,
                            fromHeadImg: 'https://ae01.alicdn.com/kf/Hd60a3f7c06fd47ae85624badd32ce54dv.jpg',
                            to: 'Taylor Swift',
                            toId: 19891221,
                            comment: '新专辑和speak now 一样棒！',
                            time: '2019年9月16日 18:43',
                            commentNum: 25,
                            like: 5,
                            inputShow: false

                        }
                    ]
                },
                {
                    name: 'Norman Fucking Rockwell',
                    id: 20190830,
                    headImg: 'https://ae01.alicdn.com/kf/Hdd856ae4c81545d2b51fa0c209f7aa28Z.jpg',
                    comment: 'Plz buy Norman Fucking Rockwell on everywhere',
                    time: '2019年9月16日 18:43',
                    commentNum: 0,
                    like: 5,
                    inputShow: false,
                    reply: []
                },
            ];
        },
        showReplyInput(item) {
            var reply = this.$refs.reply;
            if (!reply) {
                return;
            }

            this.toId = item.id;
            reply.replyName = item.name;
            reply.replyContent = item.comment;
        },
        _inputShow(i) {
            return this.comments[i].inputShow
        },
        saveComment(comment) {
            alert(comment);
        },
        dateStr(date) {
            //获取js 时间戳
            var time = new Date().getTime();
            //去掉 js 时间戳后三位，与php 时间戳保持一致
            time = parseInt((time - date) / 1000);
            //存储转换值 
            var s;
            if (time < 60 * 10) {//十分钟内
                return '刚刚';
            } else if ((time < 60 * 60) && (time >= 60 * 10)) {
                //超过十分钟少于1小时
                s = Math.floor(time / 60);
                return s + "分钟前";
            } else if ((time < 60 * 60 * 24) && (time >= 60 * 60)) {
                //超过1小时少于24小时
                s = Math.floor(time / 60 / 60);
                return s + "小时前";
            } else if ((time < 60 * 60 * 24 * 30) && (time >= 60 * 60 * 24)) {
                //超过1天少于30天内
                s = Math.floor(time / 60 / 60 / 24);
                return s + "天前";
            } else {
                //超过30天ddd
                var date0 = new Date(parseInt(date));
                return date0.getFullYear() + "/" + (date0.getMonth() + 1) + "/" + date0.getDate();
            }
        }
    },
}
</script>

<style scoped>
.sc-comment h3 {
    line-height: 31px;
}

.sc-comment .post .head {
    padding-bottom: 15px;
}

.sc-comment .reply {
    position: fixed;
    bottom: 0px;
    left: 0px;
    right: 0px;
    margin-left: 15px;
    margin-right: 15px;
    padding-left: 5px;
    padding-right: 5px;
    border: 1px solid #999;
    border-radius: 5px;
}

.sc-comment .list .head {
    padding-top: 15px;
}

.sc-comment-list {
    border-bottom: 1px dashed #efefef;
    padding-bottom: 15px;
}

.sc-comment-item {
    display: flex;
    flex-direction: row;
    flex: 1;
    flex-basis: auto;
    border-top: 1px dashed #efefef;
    margin-top: 15px;
}

.sc-comment-item .left {
    flex-shrink: 0;
    text-align: center;
    padding-top: 15px;
    padding-right: 15px;
}

.sc-comment-item .head {}

.sc-comment-item .body {
    flex-basis: 100%;
    margin-top: 10px;
}

.sc-comment-item .ref {
    background-color: #f8f8f8;
    border-radius: 5px;
    padding: 10px;
}

.sc-comment-item .comment {
    padding-top: 10px;
    padding-bottom: 10px;
}

.sc-comment-item .button {
    border-radius: 5px;
    border: 1px solid #999;
    background-color: #efefef;
    padding: 3px 8px;
}

.sc-comment-item .time {
    margin-left: 10px;
    color: #666;
    padding-top: 4px;
}

.author-title:not(:last-child) {
    border-bottom: 1px solid rgba(178, 186, 194, 0.3);
}

.author-title {
    padding: 10px;
}

.author-title .header-img {
    display: inline-block;
    vertical-align: top;
}

.author-title .author-info {
    display: inline-block;
    margin-left: 5px;
    width: 60%;
    height: 40px;
    line-height: 20px;
}

.author-title .author-info>span {
    display: block;
    cursor: pointer;
    overflow: hidden;
    white-space: nowrap;
    text-overflow: ellipsis;
}

.author-title .author-info .author-name {
    color: #000;
    font-size: 18px;
    font-weight: bold;
}

.author-title .author-info .author-time {
    font-size: 14px;
}

.author-title .icon-btn {
    width: 30%;
    padding: 0 !important;
    float: right;
    text-align: right;
}

@media screen and (max-width: 1200px) {
    .author-title .icon-btn {
        width: 20%;
        padding: 7px;
    }
}

.author-title .icon-btn>span {
    cursor: pointer;
}

.author-title .icon-btn .iconfont {
    margin: 0 5px;
}

.author-title .talk-box {
    margin: 0 50px;
}

.author-title .talk-box>p {
    margin: 0;
}

.author-title .talk-box .reply {
    font-size: 16px;
    color: #000;
}

.author-title .reply-box {
    margin: 10px 0 0 50px;
    background-color: #efefef;
}
</style>