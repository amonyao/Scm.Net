<template>
    <el-container>
        <el-header>
            123
        </el-header>
        <el-main>
            <div>
                <div class="phone-border" :style="{ width: cardWidth, height: cardHeight }">
                    <div class="phone-screen">
                        <img :src="img" />
                        <div class="datetime" :style="{ color: color }">
                            <div class="time">
                                {{ time }}
                            </div>
                            <div class="date">
                                {{ date }}
                            </div>
                        </div>
                        <div class="tool">
                            <div class="camera">
                                <el-icon>
                                    <ScIconCamera />
                                </el-icon>
                            </div>
                            <div class="finger"></div>
                            <el-button icon="sc-icon-full-screen" class="screen" @click="fullScreen"></el-button>
                        </div>
                    </div>
                    <div ref="fullScreen" class="full-screen">
                        <div>
                            <img :src="img" />
                        </div>
                        <div class="tool">
                            <el-button></el-button>
                            <el-button></el-button>
                        </div>
                    </div>
                    这里判断点击的元素是否是本身，是本身，则返回
                </div>
            </div>
        </el-main>
    </el-container>
</template>

<script>
export default {
    data() {
        return {
            isHd: true,// 是否移动设备
            cardWidth: 0,
            cardHeight: 0,

            img: 'http://localhost:2800/img/bg1.jpg',
            time: '',//当前时间
            date: '',//当前日期
            fontName: '',
            fontSize: 72,
            color: '#FF9999',//字体颜色
        };
    },
    mounted() {
        this.init();
    },
    methods: {
        init() {
            this.isHd = this.$TOOL.isMobile();
            if (this.isHd) {
                this.cardWidth = '540px';
                this.cardHeight = '960px';
            } else {
                this.cardWidth = '1040px';
                this.cardHeight = '585px';
            }

            var now = new Date();
            this.date = this.$TOOL.dateFormat(now, 'yyyy-MM-dd');
            this.time = this.$TOOL.dateFormat(now, 'HH:mm');
        },
        fullScreen() {
            this.$TOOL.screen(this.$refs.fullScreen);
        }
    },
};
</script>
<style scoped>
.phone-border {
    border-radius: 20px;
    margin: 0 auto;
    border: 3px solid #ccc;
    overflow: hidden;
    transition: 0.3s;
    background-color: #ffffff;
    box-shadow: 0px 0px 20px 1px rgba(0, 0, 0, 0.2);
}

.phone-screen {
    position: relative;
    height: 100%;
}

.phone-screen img {
    position: absolute;
    width: 100%;
    height: 100%;
}

.phone-screen .datetime {
    position: absolute;
    top: 120px;
    width: 100%;
}

.phone-screen .datetime .time {
    font-size: 72px;
    text-align: center;
}

.phone-screen .datetime .date {
    font-size: 36px;
    text-align: center;
}

.phone-screen .tool {
    position: absolute;
    bottom: 0px;
    text-align: center;
    align-items: center;
    height: 80px;
    width: 100%;
    background-color: rgba(255, 255, 255, 0.3);
}

.phone-screen .tool .finger {
    position: absolute;
    width: 80px;
    height: 80px;
    bottom: 40px;
    left: 50%;
    margin-left: -40px;
    border-radius: 40px;
    background-color: aquamarine;
}

.phone-screen .tool .camera {
    position: absolute;
    width: 48px;
    height: 48px;
    bottom: 10px;
    left: 10px;
    border-radius: 24px;
    background-color: rebeccapurple;
}

.phone-screen .tool .screen {
    position: absolute;
    width: 48px;
    height: 48px;
    bottom: 10px;
    right: 10px;
    border-radius: 24px;
    background-color: bisque;
}

.full-screen {
    text-align: center;
}
</style>