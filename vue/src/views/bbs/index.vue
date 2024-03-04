<template>
    <el-card>
        <el-row>
            <el-col>
                <h3>评论</h3>
            </el-col>
        </el-row>
        <el-row>
            <el-col>
                <myComment></myComment>
            </el-col>
        </el-row>
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
        <el-container v-for="(item, i) in comments" :key="item.id">
            <el-aside width="60px">
                <el-avatar class="header-img" :size="40" :src="item.headImg"></el-avatar>
            </el-aside>
            <el-main>
                <el-row>
                    <el-col :span="20">
                        {{ item.name }}
                    </el-col>
                    <el-col :span="4">
                        {{ item.time }}
                    </el-col>
                </el-row>
                <el-row>
                    <el-col>
                        <span @click="showReplyInput(i, item.name, item.id)">
                            {{ item.commentNum }}
                        </span>
                        {{ item.like }}
                    </el-col>
                </el-row>
                <el-row>
                    <el-col>
                        {{ item.comment }}
                    </el-col>
                </el-row>
                <el-row>
                    <el-col>
                        <el-container v-for="(reply, j) in item.reply" :key="reply.id">
                            <el-aside width="60px">
                                <el-avatar class="header-img" :size="40" :src="reply.headImg"></el-avatar>
                            </el-aside>
                            <el-main>
                                <el-row>
                                    <el-col :span="20">
                                        {{ reply.name }}
                                    </el-col>
                                    <el-col :span="4">
                                        {{ reply.time }}
                                    </el-col>
                                </el-row>
                                <el-row>
                                    <el-col>
                                        <span @click="showReplyInput(j, reply.name, reply.id)">
                                            {{ reply.commentNum }}
                                        </span>
                                        {{ reply.like }}
                                    </el-col>
                                </el-row>
                                <el-row>
                                    <el-col>
                                        {{ item.comment }}
                                    </el-col>
                                </el-row>
                            </el-main>
                        </el-container>
                    </el-col>
                </el-row>
            </el-main>
        </el-container>
        <el-row v-for="(item, i) in comments" :key="item.id" class="author-title reply-father">
            <el-col>
                <el-avatar class="header-img" :size="40" :src="item.headImg"></el-avatar>
                <div class="author-info">
                    <span class="author-name">{{ item.name }}</span>
                    <span class="author-time">{{ item.time }}</span>
                </div>
                <div class="icon-btn">
                    <span @click="showReplyInput(i, item.name, item.id)">
                        {{ item.commentNum }}
                    </span>
                    {{ item.like }}
                </div>
                <div class="talk-box">
                    <p>
                        <span class="reply">{{ item.comment }}</span>
                    </p>
                </div>
                <div class="reply-box">
                    <div v-for="(reply, j) in item.reply" :key="j" class="author-title">
                        <el-avatar class="header-img" :size="40" :src="reply.fromHeadImg"></el-avatar>
                        <div class="author-info">
                            <span class="author-name">{{ reply.from }}</span>
                            <span class="author-time">{{ reply.time }}</span>
                        </div>
                        <div class="icon-btn">
                            <span @click="showReplyInput(i, reply.from, reply.id)"><i
                                    class="iconfont el-icon-s-comment"></i>
                                {{ reply.commentNum }}</span>
                            <i class="iconfont el-icon-caret-top"></i>{{ reply.like }}
                        </div>
                        <div class="talk-box">
                            <p>
                                <span>回复 {{ reply.to }}:</span>
                                <span class="reply">{{ reply.comment }}</span>
                            </p>
                        </div>
                    </div>
                </div>
            </el-col>
        </el-row>
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
    name: 'bbs',
    components: {
        myComment: defineAsyncComponent(() => import("./components/comment")),
        ElementPlusIconsVue: ElementPlusIconsVue
    },
    data() {
        return {
            sort: '0',
            index: '0',
            replyInput: null,
            to: '',
            toId: -1,
            comments: null
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
        showReplyInput(i, name, id) {
            this.comments[this.index].inputShow = false
            this.index = i
            this.comments[i].inputShow = true
            this.to = name
            this.toId = id
        },
        _inputShow(i) {
            return this.comments[i].inputShow
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
.my-comment-reply {
    margin-left: 50px;
}

.my-comment-reply .reply-input {
    width: flex;
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