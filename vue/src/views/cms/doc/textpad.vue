<template>
    <Note ref="scNote" @readNote="readNote" @newNote="newNote" @saveNote="saveNote">
        <template #main>
            <div class="textpad">
                <el-card>
                    <template #header>
                        <div class="card-header">
                            <el-input v-model="formData.title"></el-input>
                        </div>
                    </template>
                    <el-input v-model="formData.content" type="textarea" rows="30" class="content"></el-input>
                </el-card>
            </div>
        </template>
    </Note>
</template>

<script>
import { defineAsyncComponent } from "vue";

export default {
    components: {
        Note: defineAsyncComponent(() => import("./components/note")),
    },
    data() {
        return {
            formData: this.def_data()
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

    .content {}
}
</style>