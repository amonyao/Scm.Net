<template>
    <Note ref="scNote" :types="1" @readNote="readNote" @newNote="newNote" @saveNote="saveNote">
        <div class="textpad">
            <el-card>
                <div class="textpad-title">
                    <input v-model="formData.title" :placeholder="titlePlaceholder" />
                </div>
                <div class="textpad-content">
                    <el-input v-model="formData.content" type="textarea" rows="30" class="content"
                        :placeholder="contentPlaceholder"></el-input>
                </div>
            </el-card>
        </div>
    </Note>
</template>

<script>
import { defineAsyncComponent } from "vue";

export default {
    name: 'cms_doc_textpad',
    components: {
        Note: defineAsyncComponent(() => import("./components/note")),
    },
    data() {
        return {
            formData: this.def_data(),
            titlePlaceholder: '请输入标题……',
            contentPlaceholder: '请输入内容……',
        }
    },
    mounted() {
    },
    methods: {
        def_data() {
            return {
                id: '0',
                types: 1,
                cat_id: '0',
                title: '',
                content: '',
            }
        },
        async readNote(item) {
            if (!item || !item.id) {
                return;
            }

            this.loading = true;
            var res = await this.$API.cmsdocnote.model.get(item.id);
            if (!res || res.code != 200) {
                this.loading = false;
                return;
            }

            this.formData = res.data;
        },
        newNote() {

        },
        async saveNote(catId) {
            this.formData.cat_id = catId;

            var res = await this.$API.cmsdocnote.save.post(this.formData);
            if (!res || res.code != 200) {
                return;
            }

            var note = this.$refs.scNote;
            if (note) {
                note.search();
            }
        }
    }
}
</script>
<style scoped>
.textpad {
    width: 860px;
    margin: 0 auto;

    .textpad-title {
        padding: 10px 0 20px 0;
        /* border-bottom: 1px solid #e8e8e8; */

        input {
            font-size: 30px;
            border: 0;
            outline: none;
            width: 100%;
            line-height: 1;
        }
    }

    .content {}
}
</style>