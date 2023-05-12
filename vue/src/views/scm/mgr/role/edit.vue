<template>
	<sc-dialog v-model="visible" show-fullscreen :title="titleMap[mode]" width="700px" @close="close">
		<el-form ref="formRef" label-width="100px" :model="formData" :rules="rules">
			<el-form-item label="所属机构" prop="parentId">
				<el-select v-model="formData.unit_id" placeholder="请选择所属机构" :style="{ width: '100%' }">
					<el-option v-for="item in unit_list" :key="item.value" :label="item.label" :value="item.value">
					</el-option>
				</el-select>
			</el-form-item>
			<el-form-item label="角色名称" prop="namec">
				<el-input v-model="formData.namec" clearable :maxlength="30" placeholder="请输入角色名称" show-word-limit
					:style="{ width: '100%' }" />
			</el-form-item>
			<el-form-item label="备注" prop="remark">
				<el-input v-model="formData.remark" :autosize="{ minRows: 2, maxRows: 4 }" :maxlength="500"
					placeholder="请输入备注" show-word-limit :style="{ width: '100%' }" type="textarea" />
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
			titleMap: {
				add: "新增",
				edit: "编辑",
			},
			isSaveing: false,
			visible: false,
			formData: {
				id: 0,
				unit_id: 0,
				parentId: 0,
				namec: undefined,
				remark: undefined,
				od: 1,
			},
			unit_list: [],
			rules: {
				unit_id: [
					{
						required: true,
						message: "请选择所属机构",
						trigger: "change",
					},
				],
				namec: [
					{
						required: true,
						message: "请输入角色名称",
						trigger: "blur",
					},
				],
				remark: [],
			},
		};
	},
	mounted() { },
	methods: {
		async initUnit() {
			let unitRes = await this.$API.mgrunit.option.get(0);
			this.unit_list = this.$SCM.option_one(unitRes.data);
		},
		async open(row) {
			this.initUnit();
			if (!row || !row.id) {
				this.mode = "add";
			} else {
				this.mode = "edit";
				var res = await this.$API.mgrrole.model.get(row.id);
				this.formData = res.data;
			}
			this.visible = true;
		},
		save() {
			this.$refs.formRef.validate(async (valid) => {
				if (valid) {
					this.isSaveing = true;
					let res = null;
					if (this.formData.id === 0) {
						res = await this.$API.mgrrole.add.post(this.formData);
					} else {
						res = await this.$API.mgrrole.update.put(this.formData);
					}
					this.isSaveing = false;
					if (res.code == 200) {
						this.$emit("complete");
						this.visible = false;
						this.$message.success("操作成功");
					} else {
						this.$alert(res.message, "提示", { type: "error" });
					}
				}
			});
		},
		close() {
			this.formData = {
				id: 0,
				parentId: 0,
				namec: undefined,
				remark: undefined,
				od: 1,
			};
			this.$refs.formRef.resetFields();
			this.visible = false;
		},
	},
};
</script>
<style scoped>
.cur-tip {
	display: inline-block;
	padding-left: 10px;
	color: #666;
	font-size: 12px;
}
</style>
