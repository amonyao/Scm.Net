<template>
    <el-main>
        <el-row :gutter="15">
            <el-col :lg="6">
                <myCard :title="summary0.names" :tooltip="summary0.tips" :number="summary0.number" :rate="summary0.rate"></myCard>
            </el-col>
            <el-col :lg="6">
                <myCard :title="summary1.names" :tooltip="summary1.tips" :number="summary1.number" :rate="summary1.rate"></myCard>
            </el-col>
            <el-col :lg="6">
                <myCard :title="summary2.names" :tooltip="summary2.tips" :number="summary2.number" :rate="summary2.rate"></myCard>
            </el-col>
            <el-col :lg="6">
                <myCard :title="summary3.names" :tooltip="summary3.tips" :number="summary3.number" :rate="summary3.rate"></myCard>
            </el-col>
        </el-row>
        <el-row>
            <el-col>
                <el-card shadow="never" header="数据报表">
                    <scEcharts height="300px" :option="optionCharts"></scEcharts>
                </el-card>
            </el-col>
        </el-row>
        <el-row>
            <el-col>
                <el-card shadow="never">
                    <el-tabs v-model="activeName" class="demo-tabs" @tab-click="handleClick">
                        <el-tab-pane label="我的待办" name="todo">
                            <myTask></myTask>
                        </el-tab-pane>
                        <el-tab-pane label="我的已办" name="done">
                            <myTask></myTask>
                        </el-tab-pane>
                        <el-tab-pane label="行政公告" name="note">
                            <myTask></myTask>
                        </el-tab-pane>
                        <el-tab-pane label="新闻资讯" name="news">
                            <myTask></myTask>
                        </el-tab-pane>
                        <el-tab-pane label="系统消息" name="task">
                            <myTask></myTask>
                        </el-tab-pane>
                    </el-tabs>
                </el-card>
            </el-col>
        </el-row>
    </el-main>
</template>

<script>
import myCard from './components/myCard';
import myTask from './components/myTask';
import scEcharts from "@/components/scEcharts";

export default {
    components: {
        myCard,
        myTask,
        scEcharts
    },
    data() {
        return {
            rates: [],
            summarys: [],
            summary0: {},
            summary1: {},
            summary2: {},
            summary3: {},
            optionCharts: {
                tooltip: {
                    trigger: 'axis',
                    axisPointer: { type: 'cross' }
                },
                legend: {},
                xAxis: [
                    {
                        type: 'category',
                        axisTick: {
                            alignWithLabel: true
                        },
                        data: [
                            '1月',
                            '2月',
                            '3月',
                            '4月',
                            '5月',
                            '6月',
                            '7月',
                            '8月',
                            '9月',
                            '10月',
                            '11月',
                            '12月'
                        ]
                    }
                ],
                yAxis: [
                    {
                        type: 'value',
                        name: '降水量',
                        min: 0,
                        max: 250,
                        position: 'right',
                        axisLabel: {
                            formatter: '{value} ml'
                        }
                    },
                    {
                        type: 'value',
                        name: '温度',
                        min: 0,
                        max: 25,
                        position: 'left',
                        axisLabel: {
                            formatter: '{value} °C'
                        }
                    }
                ],
                series: [
                    {
                        name: '降水量',
                        type: 'bar',
                        yAxisIndex: 0,
                        data: [6, 32, 70, 86, 68.7, 100.7, 125.6, 112.2, 78.7, 48.8, 36.0, 19.3]
                    },
                    {
                        name: '温度',
                        type: 'line',
                        smooth: true,
                        yAxisIndex: 1,
                        data: [
                            6.0,
                            10.2,
                            10.3,
                            11.5,
                            10.3,
                            13.2,
                            14.3,
                            16.4,
                            18.0,
                            16.5,
                            12.0,
                            5.2
                        ]
                    }
                ]
            },
            activeName: 'todo'
        }
    },
    mounted() {
        this.$emit('on-mounted');
        this.init();
    },
    methods: {
        init() {
            this.summarys = [{
                'names': '今日总单量', 'tips': '今日进入系统的所有订单数量', 'number': 122,
                'rate': { 'title': '环比变化：', 'value': 10 }
            },
            {
                'names': '今日待发货', 'tips': '今日进入单量中待发货数量', 'number': 122,
                'rate': { 'title': '环比变化：', 'value': 11 }
            },
            {
                'names': '月度活跃用户', 'tips': '本月所有活跃用户数量', 'number': 122,
                'rate': { 'title': '环比变化：', 'value': -21 }
            },
            {
                'names': '月度新增用户', 'tips': '本月所有新增用户数量', 'number': 122,
                'rate': { 'title': '环比变化：', 'value': 31 }
            }];
            this.summary0 = this.summarys[0];
            this.summary1 = this.summarys[1];
            this.summary2 = this.summarys[2];
            this.summary3 = this.summarys[3];
        }
    }
}
</script>