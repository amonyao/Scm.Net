<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="国别" prop="nation_id">
				<sc-select v-model="formData.nation_id" placeholder="请选择国别" :data="nation_list"
					@change="getDynasty()"></sc-select>
			</el-form-item>
			<el-form-item label="朝代" prop="dynasty_id">
				<sc-select v-model="formData.dynasty_id" placeholder="请选择朝代" :data="dynasty_list"
					@change="getAuthor()"></sc-select>
			</el-form-item>
			<el-form-item label="作者" prop="author_id">
				<sc-select v-model="formData.author_id" placeholder="请选择作者" :data="author_list"></sc-select>
			</el-form-item>
			<el-form-item label="系统名称" prop="names">
				<el-input v-model="formData.names" placeholder="请输入系统名称" :maxlength="128" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="中文名称" prop="namec">
				<el-input v-model="formData.namec" placeholder="请输入中文名称" :maxlength="128" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="English Name" prop="namee">
				<el-input v-model="formData.namee" placeholder="请输入English Name" :maxlength="256" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="显示排序" prop="od">
				<el-input-number v-model="formData.od" placeholder="请输入显示排序"></el-input-number>
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
				names: [
					{ required: true, trigger: "blur", message: "系统名称不能为空" },
				],
			},
			nation_list: [this.$SCM.OPTION_ONE],
			dynasty_list: [this.$SCM.OPTION_ONE],
			author_list: [this.$SCM.OPTION_ONE],
		};
	},
	mounted() {
		this.getNation();
	},
	methods: {
		def_data() {
			return {
				id: '0',
				nation_id: '0',
				dynasty_id: '0',
				author_id: '0',
				od: 0,
				names: '',
				namec: '',
				namee: '',
			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.cmsresorigin.edit.get(row.id);
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
						res = await this.$API.cmsresorigin.add.post(this.formData);
					} else {
						res = await this.$API.cmsresorigin.update.put(this.formData);
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
		async getNation() {
			var res = await this.$API.cmsresnation.option.get();
			this.$SCM.prepare(this.nation_list, res, false);
		},
		async getDynasty() {
			if (!this.formData.nation_id) {
				return;
			}

			var res = await this.$API.cmsresdynasty.option.get({ 'nation_id': this.formData.nation_id });
			this.$SCM.prepare(this.dynasty_list, res, false);
		},
		async getAuthor() {
			if (!this.formData.dynasty_id) {
				return;
			}

			var res = await this.$API.cmsresauthor.option.get({ 'dynasty_id': this.formData.dynasty_id });
			this.$SCM.prepare(this.author_list, res, false);
		}
	},
};
</script>

<style>
.el-select {
	width: 100%;
}
</style>