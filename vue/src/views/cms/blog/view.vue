<template>
    <div class="blog">
        <div class="title" v-text="article.title"></div>
        <div class="info">{{ article.create_name }} {{ this.$TOOL.dateTimeFormat(article.create_time) }}</div>
        <hr />
        <div class="content" v-html="article.content"></div>
    </div>
</template>
<script>
export default {
    data() {
        return {
            article: {}
        }
    },
    mounted() {
        var hid = this.$route.query.id;
        this.readBlog(hid);
    },
    methods: {
        async readBlog(id) {
            var res = await this.$API.cmsblog.model.get(id);
            if (!res || res.code != 200) {
                return;
            }

            this.article = res.data;
        }
    }
}
</script>
<style types="scss" scoped>
.blog {
    width: 850px;
    margin: 10px auto;
    background-color: #fff;
    padding: 20px 30px 20px 30px;
    border: 1px solid #e8e8e8;
    box-shadow: 0 2px 10px rgb(0 0 0 / 12%);

    .title {
        margin: 0 0 1.3rem;
        font-size: 2.667rem;
        font-weight: 600;
        line-height: 1.31;
    }

    .info {
        font-size: 14px;
    }

    hr {
        margin: 10px 0px;
    }

    .content {
        background-color: #fafafa;
    }
}
</style>