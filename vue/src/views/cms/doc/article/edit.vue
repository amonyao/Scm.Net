<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="类型" prop="types">
				<el-input v-model="formData.types" placeholder="请输入类型" :maxlength="11" show-word-limit clearable></el-input>
			</el-form-item>
			<el-form-item label="主标题" prop="title">
				<el-input v-model="formData.title" placeholder="请输入主标题" :maxlength="256" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="子标题" prop="sub_title">
				<el-input v-model="formData.sub_title" placeholder="请输入子标题" :maxlength="256" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="分类" prop="cat_id">
				<el-input v-model="formData.cat_id" placeholder="请输入分类" :maxlength="20" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="国别" prop="nation_id">
				<el-input v-model="formData.nation_id" placeholder="请输入国别" :maxlength="20" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="朝代" prop="dynasty_id">
				<el-input v-model="formData.dynasty_id" placeholder="请输入朝代" :maxlength="20" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="作者" prop="author_id">
				<el-input v-model="formData.author_id" placeholder="请输入作者" :maxlength="20" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="出处" prop="origin_id">
				<el-input v-model="formData.origin_id" placeholder="请输入出处" :maxlength="20" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="样式" prop="style_id">
				<el-input v-model="formData.style_id" placeholder="请输入样式" :maxlength="20" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="图片" prop="image">
				<el-input v-model="formData.image" placeholder="请输入图片" :maxlength="64" show-word-limit clearable></el-input>
			</el-form-item>
			<el-form-item label="文件" prop="files">
				<el-input v-model="formData.files" placeholder="请输入文件" :maxlength="11" show-word-limit clearable></el-input>
			</el-form-item>
			<el-form-item label="背景颜色" prop="back_color">
				<el-input v-model="formData.back_color" placeholder="请输入背景颜色" :maxlength="16" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="背景图像" prop="back_image">
				<el-input v-model="formData.back_image" placeholder="请输入背景图像" :maxlength="64" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="文本颜色" prop="text_color">
				<el-input v-model="formData.text_color" placeholder="请输入文本颜色" :maxlength="16" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="字体名称" prop="font_name">
				<el-input v-model="formData.font_name" placeholder="请输入字体名称" :maxlength="16" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="字体大小" prop="font_size">
				<el-input v-model="formData.font_size" placeholder="请输入字体大小" :maxlength="11" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="布局" prop="layout">
				<el-input v-model="formData.layout" placeholder="请输入布局" :maxlength="8" show-word-limit clearable></el-input>
			</el-form-item>
			<el-form-item label="排列方式" prop="origin_types">
				<el-input v-model="formData.origin_types" placeholder="请输入排列方式" :maxlength="11" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="可见" prop="public_types">
				<sc-select v-model="formData.public_types" placeholder="请输入可见"></sc-select>
			</el-form-item>

		</el-form>

		<template #footer>
			<el-button @click="close">取 消</el-button>
			<el-button :loading="isSaveing" type="primary" @click="save">
				确 定
			</el-button>
		</template>
	</sc-dialog>
</template>
<script>
export default {
	data() {
		return {
			mode: "add",
			titleMap: { add: "新增", edit: "编辑" },
			visible: false,
			isSaveing: false,
			formData: this.def_data(),
			rules: {
				codec: [
					{ required: true, trigger: "blur", message: "编码不能为空" },
				],
			},
		};
	},
	mounted() {
	},
	methods: {
		def_data() {
			return {
				id: '0',
				key: '',
				salt: '',
				types: '',
				title: '',
				sub_title: '',
				qty: '',
				qty0: '',
				fav_qty: '',
				msg_qty: '',
				cat_id: '',
				nation_id: '',
				dynasty_id: '',
				author_id: '',
				origin_id: '',
				style_id: '',
				image: '',
				files: '',
				back_color: '',
				back_image: '',
				text_color: '',
				font_name: '',
				font_size: '',
				layout: '',
				origin_types: '',
				public_types: '',

			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.cmsdocarticle.edit.get(row.id);
				this.formData = res.data;
			}
			this.visible = true;
		},
		save() {
			this.$refs.formRef.validate(async (valid) => {
				if (valid) {
					this.isSaveing = true;
					let res = null;
					if (this.formData.id === '0') {
						res = await this.$API.cmsdocarticle.add.post(this.formData);
					} else {
						res = await this.$API.cmsdocarticle.update.put(this.formData);
					}
					this.isSaveing = false;
					if (res.code == 200) {
						this.$emit("complete");
						this.visible = false;
						this.$message.success("保存成功");
					} else {
						this.$alert(res.message, "提示", { type: "error" });
					}
				}
			});
		},
		close() {
			this.formData = this.def_data();
			this.$refs.formRef.resetFields();
			this.visible = false;
		},
	},
};
</script>

<style>
.el-select {
	width: 100%;
}
</style>