<template>
	<el-container>
		<el-main>
			<sc-list :data="list">
				<template #item="{ item }">
					<div class="list-item-text">
						{{ item.title }}
					</div>
					<span>{{ item.summary }}</span>
				</template>
			</sc-list>
		</el-main>
	</el-container>
</template>
<script>
export default {
	data() {
		return {
			apiObj: this.$API.cmsdocarticleshare.page,
			list: [],
			param: {
				types: 20,
				row_status: '1',
				create_time: '',
				key: ''
			},
		};
	},
	mounted() {
		this.search();
	},
	methods: {
		async search() {
			var res = await this.$API.cmsdocarticleshare.page.get(this.param);
			if (!res || res.code != 200) {
				return;
			}
			this.list = res.data;
		},
		selectionChange(selection) {
			this.selection = selection;
		},
	},
};
</script>
