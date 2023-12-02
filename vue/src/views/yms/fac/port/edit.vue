<template>
	<sc-dialog v-model="visible" show-fullscreen destroy-on-close :title="titleMap[mode]" width="750px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="所属园区" prop="area_id">
				<sc-select v-model="formData.area_id" :data="area_list"></sc-select>
			</el-form-item>
			<el-form-item label="大门类型" prop="types">
				<sc-select v-model="formData.types" :data="types_list"></sc-select>
			</el-form-item>
			<el-form-item label="出入模式" prop="modes">
				<sc-select v-model="formData.modes" :data="modes_list"></sc-select>
			</el-form-item>
			<el-form-item label="大门编码" prop="codec">
				<el-input v-model="formData.codec" placeholder="请输入大门编码" :maxlength="32" show-word-limit
					clearable></el-input>
			</el-form-item>
			<el-form-item label="大门名称" prop="namec">
				<el-input v-model="formData.namec" placeholder="请输入大门名称" :maxlength="32" show-word-limit
					clearable></el-input>
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
				area_id: [
					{ required: true, trigger: "blur", pattern: this.$SCM.REGEX_ID, message: "请选择园区" },
				],
				codec: [
					{ required: true, trigger: "blur", message: "大门编码不能为空" },
				],
				namec: [
					{ required: true, trigger: "blur", message: "大门名称不能为空" },
				],
			},
			area_list: [],
			types_list: [],
			modes_list: [],
		};
	},
	mounted() {
		this.list_area();
		this.$SCM.list_dic(this.types_list, 'yms_fav_port_types', false);
		this.$SCM.list_dic(this.modes_list, 'yms_fav_port_modes', false);
	},
	methods: {
		def_data() {
			return {
				id: '0',
				area_id: '0',
				types: '0',
				modes: '0',
				codec: '',
				namec: '',
			}
		},
		async open(row) {
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.ymsfacport.edit.get(row.id);
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
						res = await this.$API.ymsfacport.add.post(this.formData);
					} else {
						res = await this.$API.ymsfacport.update.put(this.formData);
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
		async list_area() {
			this.$SCM.list_option(this.area_list, this.$API.ymsfacarea.option, {}, false);
		}
	},
};
</script>

<style>
.el-select {
	width: 100%;
}
</style>